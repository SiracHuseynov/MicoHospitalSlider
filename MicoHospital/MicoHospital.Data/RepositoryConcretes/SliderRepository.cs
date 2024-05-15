using MicoHospital.Core.Models;
using MicoHospital.Core.RepositoryAbstracts;
using MicoHospital.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicoHospital.Data.RepositoryConcretes
{
    public class SliderRepository : GenericRepository<Slider>, ISliderRepository
    {
        public SliderRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
