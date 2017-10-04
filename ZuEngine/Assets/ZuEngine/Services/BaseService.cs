using System.Collections;
using System.Collections.Generic;

namespace ZuEngine.Service
{
	public class BaseService<T> where T: class , new()
	{
		private static T m_instance;

		public static T Instance
		{
			get{
				if ( m_instance == null ) 
				{
					m_instance = new T ();
				}
				return m_instance;
			}
		}
	}
}
