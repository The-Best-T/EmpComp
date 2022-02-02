namespace EmpComp.Models
{
    public class Company : Entity
    {
        public string Name { get; set; }
        public virtual List<Employee> Employees { get; set; } = new();
    }
}
