using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZuEngine.Service
{
	public class ResourceService : BaseService<ResourceService>
	{
		public Object Load(string path)
		{
			return Resources.Load (path);
		}
	}
}
