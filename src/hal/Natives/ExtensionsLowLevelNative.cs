using System;
using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    public unsafe class ExtensionsLowLevelNative
    {

        private readonly delegate* unmanaged[Cdecl]<int> HAL_LoadExtensionsFunc;
        public int HAL_LoadExtensions()
        {
            return HAL_LoadExtensionsFunc();
        }

        private readonly delegate* unmanaged[Cdecl]<byte*, int> HAL_LoadOneExtensionFunc;

        public int HAL_LoadOneExtension(byte* library)
        {
            return HAL_LoadOneExtensionFunc(library);
        }

        private readonly delegate* unmanaged[Cdecl]<int, void> HAL_SetShowExtensionsNotFoundMessagesFunc;

        public void HAL_SetShowExtensionsNotFoundMessages(int showMessage)
        {
            HAL_SetShowExtensionsNotFoundMessagesFunc(showMessage);
        }

        public ExtensionsLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_LoadExtensionsFunc = (delegate* unmanaged[Cdecl] < int >)loader.GetProcAddress("HAL_LoadExtensions");
            HAL_LoadOneExtensionFunc = (delegate* unmanaged[Cdecl] < byte *, int >)loader.GetProcAddress("HAL_LoadOneExtension");
            HAL_SetShowExtensionsNotFoundMessagesFunc = (delegate* unmanaged[Cdecl] < int, void >)loader.GetProcAddress("HAL_SetShowExtensionsNotFoundMessages");
        }
    }
}
