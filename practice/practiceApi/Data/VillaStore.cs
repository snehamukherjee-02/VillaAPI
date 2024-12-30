using practiceApi.Models.Dto;

namespace practiceApi.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>()
            {
                new VillaDTO(){Id=1,Name="Pool Villa",Sqft=300,Occupency=5},
                new VillaDTO(){Id=2,Name="Beach Villa",Sqft=400,Occupency=5}
            };
    }
}
