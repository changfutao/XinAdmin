using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xin.Service.Image.Dto
{
    public class ImageDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public long ImageCategoryId { get; set; }
        public string? Path { get; set; }
    }
}
