namespace SGWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public Double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

        // Navigation 

        public Region Region { get; set; }
        public WalkDifficulty WalkDifficulty { get; set; }    
    }

}
