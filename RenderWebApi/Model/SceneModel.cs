using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RenderWebApi.Model
{
    public class SceneModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        public double[] Center { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        public double[] Direction { get; set; }

        [Required]
        public double PixelSpacing { get; set; }
    }
}
