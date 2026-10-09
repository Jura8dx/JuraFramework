using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuraFramework.Graphics;

/// <summary>
/// Containers are simply Drawables that get skipped in the rendering process.
/// Children are still rendererd unless this drawable is inactive.
/// </summary>
public class Container : Drawable
{
}
