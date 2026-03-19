namespace FileDemo.Api.Models
{
    public class FileUploadModel
    {
        // The IFormFile interface is used to represent a file sent with the HttpRequest. It provides properties and methods to access the file's content, name, and other metadata.
                public IFormFile File { get; set; }

                public string Name { get; set; }
                public string Description { get; set; } 
                public string Author { get; set; }  
    }   
}