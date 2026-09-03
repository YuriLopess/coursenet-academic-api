namespace CourseNet.Academic.Core.Domain.Entities;

public class Course
{
    public int Id { get; private set; }
    public string Titulo { get; set; }
    public string Url { get; set; }
    public string Icone { get; set; }
    public bool Assistido { get; set; }

    public Course(string titulo, string url, string icone, bool assistido = false)
    {
        Titulo = titulo;
        Url = url;
        Icone = icone;
        Assistido = assistido;
    }
}
