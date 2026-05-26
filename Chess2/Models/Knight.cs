using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess2.Models
{
    internal class Knight : Piece
    {
        public override string Symbol => IsWhite ? "♘" : "♞";
    }
}
