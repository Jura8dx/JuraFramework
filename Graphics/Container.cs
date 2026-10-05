using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuraFramework.Graphics
{
    /// <summary>
    /// Contains are derived from <see cref="Drawable"/> but get skipped in the rendering process.
    /// Children of this <see cref="Container"/> will still be rendered
    /// </summary>
    public class Container : Drawable
    {
    }
}
