namespace TravelPacker.Util {
	/// <summary>
	/// Represents different Windows 10 device families
	/// </summary>
	public enum DeviceFamily {
		Desktop,
		Mobile
	}

	/// <summary>
	/// Retrieves strongly-typed device family
	/// </summary>
	public static class DeviceFamilyRecognizer {
		static DeviceFamilyRecognizer() {
			DeviceFamily = RecognizeDeviceFamily(Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily);
		}

		public static DeviceFamily DeviceFamily { get; }

		private static DeviceFamily RecognizeDeviceFamily(string deviceFamily) {
			switch (deviceFamily) {
				case "Windows.Mobile":
				return DeviceFamily.Mobile;
				default:
				return DeviceFamily.Desktop;
			}
		}
	}
}