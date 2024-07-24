namespace BlazorApp1.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int ProjectId { get; set; }
    }
    public class TareaSaveDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Proyecto { get; set; }
    }
    public class TareaUpdateDto
    {
        public string Status { get; set; }
    }
}
