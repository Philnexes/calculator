using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gui
{
    public class Operation
    {
        #region Public properties
        public string LeftSide { get; set; }

        public string RightSide { get; set; }

        public OperationType OperationType { get; set; }

        public Operation InnerOperation { get; set; }

        #endregion

        public Operation()
        {
            this.LeftSide = string.Empty;
            this.RightSide = string.Empty;
        }
    }
}