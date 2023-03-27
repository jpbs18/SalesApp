namespace WebApp.Models
{
    public class Department
    {
        public long Id { get; private set; }
        public string Name { get; private set; }

        public Department(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
