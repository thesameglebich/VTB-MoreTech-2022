namespace WebApi.DataAccessLayer.Models
{
    public enum UserRole
    {
        Worker = 0, // обычный сотрудник, зарегистрированный в системе
        Editor = 1, // реадактор
        Leader = 2, // руководитель
        Superadmin = 3 // суперадминистратор системы
    };
}
