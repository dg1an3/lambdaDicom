namespace Samples.FSharp.HelloWorldTypeProvider

open System
open System.Reflection
open ProviderImplementation.ProvidedTypes
open FSharp.Core.CompilerServices
open FSharp.Quotations

// This type defines the type provider. When compiled to a DLL, it can be added
// as a reference to an F# command-line compilation, script, or project.
[<TypeProvider>]
type SampleTypeProvider(config: TypeProviderConfig) as this = 

  // Inheriting from this type provides implementations of ITypeProvider 
  // in terms of the provided types below.
  inherit TypeProviderForNamespaces(config)

  let namespaceName = "Samples.HelloWorldTypeProvider"
  let thisAssembly = Assembly.GetExecutingAssembly()

  // Make one provided type, called TypeN.
  let makeOneProvidedType (n:int) = 

    // This is the provided type. It is an erased provided type and, in compiled code, 
    // will appear as type 'obj'.
    let t = ProvidedTypeDefinition(thisAssembly, namespaceName, "Type" + string n, baseType = Some typeof<obj>)
    t.AddXmlDocDelayed (fun () -> sprintf "This provided type %s" ("Type" + string n))

    let staticProp = ProvidedProperty(propertyName = "StaticProperty", propertyType = typeof<string>, isStatic = true, getterCode = (fun args -> <@@ "Hello!" @@>))
    staticProp.AddXmlDocDelayed(fun () -> "This is a static property")

    let ctor = ProvidedConstructor(parameters = [ ], invokeCode = (fun args -> <@@ "The object data" :> obj @@>))
    ctor.AddXmlDocDelayed(fun () -> "This is a constructor")
    t.AddMember ctor

    let ctor2 = ProvidedConstructor(parameters = [ ProvidedParameter("data",typeof<string>) ], invokeCode = (fun args -> <@@ (%%(args.[0]) : string) :> obj @@>))
    t.AddMember ctor2

    let instanceProp = ProvidedProperty(propertyName = "InstanceProperty", propertyType = typeof<int>, getterCode= (fun args -> <@@ ((%%(args.[0]) : obj) :?> string).Length @@>))
    instanceProp.AddXmlDocDelayed(fun () -> "This is an instance property")
    t.AddMember instanceProp

    let instanceMeth = ProvidedMethod(methodName = "InstanceMethod", parameters = [ProvidedParameter("x",typeof<int>)], returnType = typeof<char>, invokeCode = (fun args -> <@@ ((%%(args.[0]) : obj) :?> string).Chars(%%(args.[1]) : int) @@>))

    instanceMeth.AddXmlDocDelayed(fun () -> "This is an instance method")
    // Add the instance method to the type.
    t.AddMember instanceMeth

    t.AddMembersDelayed(fun () -> 
      let nestedType = ProvidedTypeDefinition("NestedType", Some typeof<obj>)

      nestedType.AddMembersDelayed (fun () -> 
        let staticPropsInNestedType = 
          [ for i in 1 .. 100 do
              let valueOfTheProperty = "I am string "  + string i

              let p = 
                ProvidedProperty(propertyName = "StaticProperty" + string i, 
                  propertyType = typeof<string>, 
                  isStatic = true,
                  getterCode= (fun args -> <@@ valueOfTheProperty @@>))

              p.AddXmlDocDelayed(fun () -> 
                  sprintf "This is StaticProperty%d on NestedType" i)

              yield p ]

        staticPropsInNestedType)

      [nestedType])
    t

  // Now generate 100 types
  let types = [ for i in 1 .. 100 -> makeOneProvidedType i ] 

  // And add them to the namespace
  do this.AddNamespace(namespaceName, types)

[<assembly:TypeProviderAssembly>] 
do()