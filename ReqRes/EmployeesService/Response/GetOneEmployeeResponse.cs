namespace EmpComp.ReqRes.EmployeesService.Response
{
    public class GetOneEmployeeResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
    }
}
