using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public abstract class Piece
    {
        public bool IsWhite { get; set; }
        public abstract string Symbol { get; }
    }
}
