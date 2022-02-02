namespace EmpComp.Models
{
    public class Employee : Entity
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Age { get; set; }
        public Guid? CompanyId { get; set; }
        public virtual Company? Company { get; set; }
    }
}
