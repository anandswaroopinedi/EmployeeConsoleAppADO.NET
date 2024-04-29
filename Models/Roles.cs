namespace Models
{
    public class Roles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
    }
}
