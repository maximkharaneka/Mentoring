using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector
{
    public class FileChangeDTO
    {
        private string path;

        public FileChangeDTO()
        {
        }

        public FileChangeDTO(string path)
        {
            this.path = path;
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public override string ToString()
        {
            return this.Path;
        }
    }
}
