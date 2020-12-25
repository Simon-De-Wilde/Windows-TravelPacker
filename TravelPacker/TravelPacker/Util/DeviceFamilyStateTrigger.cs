﻿using Windows.UI.Xaml;

namespace TravelPacker.Util {
	/// <summary>
	/// Trigger to differentiate between device families
	/// </summary>
	public class DeviceFamilyStateTrigger : StateTriggerBase {
		public static readonly DependencyProperty TargetDeviceFamilyProperty = DependencyProperty.Register(
			"TargetDeviceFamily", typeof(DeviceFamily), typeof(DeviceFamilyStateTrigger), new PropertyMetadata(default(DeviceFamily), OnDeviceTypePropertyChanged));

		public DeviceFamily TargetDeviceFamily {
			get { return (DeviceFamily)GetValue(TargetDeviceFamilyProperty); }
			set { SetValue(TargetDeviceFamilyProperty, value); }
		}

		private static void OnDeviceTypePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs) {
			DeviceFamilyStateTrigger trigger = (DeviceFamilyStateTrigger)dependencyObject;
			DeviceFamily newTargetDeviceFamily = (DeviceFamily)eventArgs.NewValue;
			trigger.SetActive(newTargetDeviceFamily == DeviceFamilyRecognizer.DeviceFamily);
		}
	}
}