using System.Collections.Generic;

namespace SpaceWalker.Services.Ota;

public class OtaRule
{
	public string rule_id { get; set; }

	public string reason { get; set; }

	public string min_disabled_app_version { get; set; }

	public string max_disabled_app_version { get; set; }

	public List<string> pids { get; set; }

	public string action { get; set; }
}
