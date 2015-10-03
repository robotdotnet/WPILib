namespace NIVision.IMAQdx
{
    public enum IMAQdxError : uint
    {
        IMAQdxErrorSuccess = 0x0,                   // Success
        IMAQdxErrorSystemMemoryFull = 0xBFF69000,   // Not enough memory
        IMAQdxErrorInternal,                        // Internal error
        IMAQdxErrorInvalidParameter,                // Invalid parameter
        IMAQdxErrorInvalidPointer,                  // Invalid pointer
        IMAQdxErrorInvalidInterface,                // Invalid camera session
        IMAQdxErrorInvalidRegistryKey,              // Invalid registry key
        IMAQdxErrorInvalidAddress,                  // Invalid address
        IMAQdxErrorInvalidDeviceType,               // Invalid device type
        IMAQdxErrorNotImplemented,                  // Not implemented
        IMAQdxErrorCameraNotFound,                  // Camera not found
        IMAQdxErrorCameraInUse,                     // Camera is already in use.
        IMAQdxErrorCameraNotInitialized,            // Camera is not initialized.
        IMAQdxErrorCameraRemoved,                   // Camera has been removed.
        IMAQdxErrorCameraRunning,                   // Acquisition in progress.
        IMAQdxErrorCameraNotRunning,                // No acquisition in progress.
        IMAQdxErrorAttributeNotSupported,           // Attribute not supported by the camera.
        IMAQdxErrorAttributeNotSettable,            // Unable to set attribute.
        IMAQdxErrorAttributeNotReadable,            // Unable to get attribute.
        IMAQdxErrorAttributeOutOfRange,             // Attribute value is out of range.
        IMAQdxErrorBufferNotAvailable,              // Requested buffer is unavailable.
        IMAQdxErrorBufferListEmpty,                 // Buffer list is empty. Add one or more buffers.
        IMAQdxErrorBufferListLocked,                // Buffer list is already locked. Reconfigure acquisition and try again.
        IMAQdxErrorBufferListNotLocked,             // No buffer list. Reconfigure acquisition and try again.
        IMAQdxErrorResourcesAllocated,              // Transfer engine resources already allocated. Reconfigure acquisition and try again.
        IMAQdxErrorResourcesUnavailable,            // Insufficient transfer engine resources.
        IMAQdxErrorAsyncWrite,                      // Unable to perform asychronous register write.
        IMAQdxErrorAsyncRead,                       // Unable to perform asychronous register read.
        IMAQdxErrorTimeout,                         // Timeout.
        IMAQdxErrorBusReset,                        // Bus reset occurred during a transaction.
        IMAQdxErrorInvalidXML,                      // Unable to load camera's XML file.
        IMAQdxErrorFileAccess,                      // Unable to read/write to file.
        IMAQdxErrorInvalidCameraURLString,          // Camera has malformed URL string.
        IMAQdxErrorInvalidCameraFile,               // Invalid camera file.
        IMAQdxErrorGenICamError,                    // Unknown Genicam error.
        IMAQdxErrorFormat7Parameters,               // For format 7: The combination of speed, image position, image size, and color coding is incorrect.
        IMAQdxErrorInvalidAttributeType,            // The attribute type is not compatible with the passed variable type.
        IMAQdxErrorDLLNotFound,                     // The DLL could not be found.
        IMAQdxErrorFunctionNotFound,                // The function could not be found.
        IMAQdxErrorLicenseNotActivated,             // License not activated.
        IMAQdxErrorCameraNotConfiguredForListener,  // The camera is not configured properly to support a listener.
        IMAQdxErrorCameraMulticastNotAvailable,     // Unable to configure the system for multicast support.
        IMAQdxErrorBufferHasLostPackets,            // The requested buffer has lost packets and the user requested an error to be generated.
        IMAQdxErrorGiGEVisionError,                 // Unknown GiGE Vision error.
        IMAQdxErrorNetworkError,                    // Unknown network error.
        IMAQdxErrorCameraUnreachable,               // Unable to connect to the camera.
        IMAQdxErrorHighPerformanceNotSupported,     // High performance acquisition is not supported on the specified network interface. Connect the camera to a network interface running the high performance driver.
        IMAQdxErrorInterfaceNotRenamed,             // Unable to rename interface. Invalid or duplicate name specified.
        IMAQdxErrorNoSupportedVideoModes,           // The camera does not have any video modes which are supported.
        IMAQdxErrorSoftwareTriggerOverrun,          // Software trigger overrun.
        IMAQdxErrorTestPacketNotReceived,           // The system did not receive a test packet from the camera. The packet size may be too large for the network configuration or a firewall may be enabled.
        IMAQdxErrorCorruptedImageReceived,          // The camera returned a corrupted image.
        IMAQdxErrorCameraConfigurationHasChanged,   // The camera did not return an image of the correct type it was configured for previously.
        IMAQdxErrorCameraInvalidAuthentication,     // The camera is configured with password authentication and either the user name and password were not configured or they are incorrect.
        IMAQdxErrorUnknownHTTPError,                // The camera returned an unknown HTTP error.
        IMAQdxErrorKernelDriverUnavailable,         // Unable to attach to the kernel mode driver.
        IMAQdxErrorPixelFormatDecoderUnavailable,   // No decoder available for selected pixel format.
        IMAQdxErrorFirmwareUpdateNeeded,            // The acquisition hardware needs a firmware update before it can be used.
        IMAQdxErrorFirmwareUpdateRebootNeeded,      // The firmware on the acquisition hardware has been updated and the system must be rebooted before use.
        IMAQdxErrorLightingCurrentOutOfRange,       // The requested current level from the lighting controller is not possible.
        IMAQdxErrorUSB3VisionError,                 // Unknown USB3 Vision error.
        IMAQdxErrorInvalidU3VUSBDescriptor,         // The camera has a USB descriptor that is incompatible with the USB3 Vision specification.
        IMAQdxErrorU3VInvalidControlInterface,      // The USB3 Vision control interface is not implemented or is invalid on this camera.
        IMAQdxErrorU3VControlInterfaceError,        // There was an error from the control interface of the USB3 Vision camera.
        IMAQdxErrorU3VInvalidEventInterface,        // The USB3 Vision event interface is not implemented or is invalid on this camera.
        IMAQdxErrorU3VEventInterfaceError,          // There was an error from the event interface of the USB3 Vision camera.
        IMAQdxErrorU3VInvalidStreamInterface,       // The USB3 Vision stream interface is not implemented or is invalid on this camera.
        IMAQdxErrorU3VStreamInterfaceError,         // There was an error from the stream interface of the USB3 Vision camera.
        IMAQdxErrorU3VUnsupportedConnectionSpeed,   // The USB connection speed is not supported by the camera.  Check whether the camera is plugged into a USB 2.0 port instead of a USB 3.0 port.  If so, verify that the camera supports this use case.       
        IMAQdxErrorU3VInsufficientPower,            // The USB3 Vision camera requires more current than can be supplied by the USB port in use.
        IMAQdxErrorU3VInvalidMaxCurrent,            // The U3V_MaximumCurrentUSB20_mA registry value is not valid for the connected USB3 Vision camera.
        IMAQdxErrorBufferIncompleteData,            // The requested buffer has incomplete data and the user requested an error to be generated.
        IMAQdxErrorCameraAcquisitionConfigFailed,   // The camera returned an error starting the acquisition.
        IMAQdxErrorCameraClosePending,              // The camera still has outstanding references and will be closed when these operations complete.
        IMAQdxErrorSoftwareFault,                   // An unexpected software error occurred.
        IMAQdxErrorCameraPropertyInvalid,           // The value for an invalid camera property was requested.
        IMAQdxErrorJumboFramesNotEnabled,           // Jumbo frames are not enabled on the host.  Maximum packet size is 1500 bytes.
        IMAQdxErrorBayerPixelFormatNotSelected,     // This operation requires that the camera has a Bayer pixel format selected.
        IMAQdxErrorGuard = 0xFFFFFFFF,
    }

    public enum IMAQdxBusType 
    {
        @IMAQdxBusTypeFireWire = 825440564,
        @IMAQdxBusTypeEthernet = 1768977972,
        @IMAQdxBusTypeSimulator = 544434541,
        @IMAQdxBusTypeDirectShow = 1685284983,
        @IMAQdxBusTypeIP = 1230005101,
        @IMAQdxBusTypeSmartCam2 = 1396924722,
        @IMAQdxBusTypeUSB3Vision = 1431519795,
        @IMAQdxBusTypeUVC = 1431716640,
        @IMAQdxBusTypeGuard = -1,
    }

    public enum IMAQdxCameraControlMode 
    {
        @IMAQdxCameraControlModeController = 0,
        @IMAQdxCameraControlModeListener = 1,
        @IMAQdxCameraControlModeGuard = -1,
    }

    public enum IMAQdxBufferNumberMode 
    {
        @IMAQdxBufferNumberModeNext = 0,
        @IMAQdxBufferNumberModeLast = 1,
        @IMAQdxBufferNumberModeBufferNumber = 2,
        @IMAQdxBufferNumberModeGuard = -1,
    }

    public enum IMAQdxPnpEvent 
    {
        @IMAQdxPnpEventCameraAttached = 0,
        @IMAQdxPnpEventCameraDetached = 1,
        @IMAQdxPnpEventBusReset = 2,
        @IMAQdxPnpEventGuard = -1,
    }

    public enum IMAQdxBayerPattern 
    {
        @IMAQdxBayerPatternNone = 0,
        @IMAQdxBayerPatternGB = 1,
        @IMAQdxBayerPatternGR = 2,
        @IMAQdxBayerPatternBG = 3,
        @IMAQdxBayerPatternRG = 4,
        @IMAQdxBayerPatternHardware = 5,
        @IMAQdxBayerPatternGuard = -1,
    }

    public enum IMAQdxBayerAlgorithm 
    {
        @IMAQdxBayerAlgorithmBilinear = 0,
        @IMAQdxBayerAlgorithmVNG = 1,
        @IMAQdxBayerAlgorithmGuard = -1,
    }

    public enum IMAQdxOutputImageType 
    {
        @IMAQdxOutputImageTypeU8 = 0,
        @IMAQdxOutputImageTypeI16 = 1,
        @IMAQdxOutputImageTypeU16 = 7,
        @IMAQdxOutputImageTypeRGB32 = 4,
        @IMAQdxOutputImageTypeRGB64 = 6,
        @IMAQdxOutputImageTypeAuto = 2147483647,
        @IMAQdxOutputImageTypeGuard = -1,
    }

    public enum IMAQdxDestinationMode 
    {
        @IMAQdxDestinationModeUnicast = 0,
        @IMAQdxDestinationModeBroadcast = 1,
        @IMAQdxDestinationModeMulticast = 2,
        @IMAQdxDestinationModeGuard = -1,
    }

    public enum IMAQdxAttributeType 
    {
        @IMAQdxAttributeTypeU32 = 0,
        @IMAQdxAttributeTypeI64 = 1,
        @IMAQdxAttributeTypeF64 = 2,
        @IMAQdxAttributeTypeString = 3,
        @IMAQdxAttributeTypeEnum = 4,
        @IMAQdxAttributeTypeBool = 5,
        @IMAQdxAttributeTypeCommand = 6,
        @IMAQdxAttributeTypeBlob = 7,
        @IMAQdxAttributeTypeGuard = -1,
    }

    public enum IMAQdxValueType 
    {
        @IMAQdxValueTypeU32 = 0,
        @IMAQdxValueTypeI64 = 1,
        @IMAQdxValueTypeF64 = 2,
        @IMAQdxValueTypeString = 3,
        @IMAQdxValueTypeEnumItem = 4,
        @IMAQdxValueTypeBool = 5,
        @IMAQdxValueTypeDisposableString = 6,
        @IMAQdxValueTypeGuard = -1,
    }

    public enum IMAQdxInterfaceFileFlags 
    {
        @IMAQdxInterfaceFileFlagsConnected = 1,
        @IMAQdxInterfaceFileFlagsDirty = 2,
        @IMAQdxInterfaceFileFlagsGuard = -1,
    }

    public enum IMAQdxOverwriteMode 
    {
        @IMAQdxOverwriteModeGetOldest = 0,
        @IMAQdxOverwriteModeFail = 2,
        @IMAQdxOverwriteModeGetNewest = 3,
        @IMAQdxOverwriteModeGuard = -1,
    }

    public enum IMAQdxIncompleteBufferMode 
    {
        @IMAQdxIncompleteBufferModeIgnore = 0,
        @IMAQdxIncompleteBufferModeFail = 1,
        @IMAQdxIncompleteBufferModeGuard = -1,
    }

    public enum IMAQdxLostPacketMode 
    {
        @IMAQdxLostPacketModeIgnore = 0,
        @IMAQdxLostPacketModeFail = 1,
        @IMAQdxLostPacketModeGuard = -1,
    }

    public enum IMAQdxAttributeVisibility 
    {
        @IMAQdxAttributeVisibilitySimple = 4096,
        @IMAQdxAttributeVisibilityIntermediate = 8192,
        @IMAQdxAttributeVisibilityAdvanced = 16384,
        @IMAQdxAttributeVisibilityGuard = -1,
    }

    public enum IMAQdxStreamChannelMode 
    {
        @IMAQdxStreamChannelModeAutomatic = 0,
        @IMAQdxStreamChannelModeManual = 1,
        @IMAQdxStreamChannelModeGuard = -1,
    }

    public enum IMAQdxPixelSignedness 
    {
        @IMAQdxPixelSignednessUnsigned = 0,
        @IMAQdxPixelSignednessSigned = 1,
        @IMAQdxPixelSignednessHardware = 2,
        @IMAQdxPixelSignednessGuard = -1,
    }

    public enum IMAQdxUSBConnectionSpeed 
    {
        @IMAQdxUSBConnectionSpeedLow = 1,
        @IMAQdxUSBConnectionSpeedFull = 2,
        @IMAQdxUSBConnectionSpeedHigh = 4,
        @IMAQdxUSBConnectionSpeedSuper = 8,
        @IMAQdxUSBConnectionSpeedGuard = -1,
    }
}
