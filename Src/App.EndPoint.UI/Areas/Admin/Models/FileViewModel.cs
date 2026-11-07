public class FileViewModel
{
    public  int  Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string? Extention{ get; set; }
    public string Type { get; set; }
    public Stream Content { get; set; }
}
