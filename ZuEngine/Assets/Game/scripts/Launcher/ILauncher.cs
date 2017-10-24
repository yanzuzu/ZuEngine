using System.Collections;
using System.Collections.Generic;

public interface ILauncher
{
	void SetTarget(ILauncherTarget target);
	void Fire();
}
