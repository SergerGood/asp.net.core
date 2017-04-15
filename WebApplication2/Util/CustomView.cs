using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ASP.NET.Sample.Web.Util
{
    public class CustomView : IView
    {
        public CustomView(string viewPath)
        {
            Path = viewPath;
        }

        public string Path { get; set; }

        public async Task RenderAsync(ViewContext context)
        {
            string content = string.Empty;
            using (FileStream viewStream = new FileStream(Path, FileMode.Open))
            {
                using (StreamReader viewReader = new StreamReader(viewStream))
                {
                    content = viewReader.ReadToEnd();
                }
            }
            await context.Writer.WriteAsync(content);
        }
    }
}
