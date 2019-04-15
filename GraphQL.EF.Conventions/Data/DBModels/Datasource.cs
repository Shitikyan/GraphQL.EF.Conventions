namespace GraphQL.EF.Conventions.Data.Models
{
    public class Datasource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public int ProjectId { get; set; }
    }
}
