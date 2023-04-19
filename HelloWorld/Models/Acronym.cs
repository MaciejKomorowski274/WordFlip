using SQLite;


namespace HelloWorld.Models
{
    [Table("acronym")]
    public class Acronym
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string TranslationEnglish { get; set; }
        public string TranslationPolish { get; set; }
        public Category Type { get; set; }
    }
    public enum Category
    {
        Sieci,
        Systemy
    }
}
