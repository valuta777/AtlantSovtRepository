using System;
using System.Resources;
using System.Text;
using System.Reflection;
using System.Globalization;

namespace MultiLang
{
  internal class ml
  {
    private static string RootNamespace = "YourProject" ; //MLHIDE
    private static ResourceManager   ResMgr ;

    static ml()
    {
      if ( RootNamespace.Length > 0 )
        ResMgr = new ResourceManager ( RootNamespace + ".MultiLang", Assembly.GetExecutingAssembly ( ) ) ; //MLHIDE
      else
        ResMgr = new ResourceManager ( "MultiLang", Assembly.GetExecutingAssembly ( ) );                   //MLHIDE
    }

    public static void ml_UseCulture ( CultureInfo ci )
    {
      System.Threading.Thread.CurrentThread.CurrentUICulture = ci ;
    }
    
    public static string ml_string ( int StringID, string Text )
    {
      return ml_resource ( StringID ) ;
    }

    public static string ml_resource ( int StringID )
    {
      return ResMgr.GetString ( "_" + StringID.ToString() ) ;
    }
  }

  internal class ml_TableAttribute : System.ComponentModel.DataAnnotations.Schema.TableAttribute
  {
    public ml_TableAttribute( int StringID, string Text )
      : base ( ml.ml_string ( StringID, Text ) )
    {
    }
  }

  internal class ml_ColumnAttribute : System.ComponentModel.DataAnnotations.Schema.ColumnAttribute
  {
    public ml_ColumnAttribute( int StringID, string Text )
      : base ( ml.ml_string ( StringID, Text ) )
    {
    }
  }
}
