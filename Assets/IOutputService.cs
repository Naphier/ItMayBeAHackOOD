using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Simple interface contract to send output to some form of console.
/// </summary>
public interface IOutputService
{
    /// <summary>
    /// Write any generic object (should have ToString applied).
    /// </summary>
    /// <param name="value"></param>
    void Write(object value);

    /// <summary>
    /// Write output using string.Format() or similar string sonstruction method.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="values"></param>
    void WriteFormat(string value, params object[] values);
}
