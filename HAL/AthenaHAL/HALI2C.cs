using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
internal class HALI2C
{
internal static void Initialize(IntPtr library, ILibraryLoader loader)
{

Base.HALI2C.HAL_InitializeI2C = (Base.HALI2C.HAL_InitializeI2CDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_InitializeI2C"), typeof(Base.HALI2C.HAL_InitializeI2CDelegate));

Base.HALI2C.HAL_TransactionI2C = (Base.HALI2C.HAL_TransactionI2CDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_TransactionI2C"), typeof(Base.HALI2C.HAL_TransactionI2CDelegate));

Base.HALI2C.HAL_WriteI2C = (Base.HALI2C.HAL_WriteI2CDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_WriteI2C"), typeof(Base.HALI2C.HAL_WriteI2CDelegate));

Base.HALI2C.HAL_ReadI2C = (Base.HALI2C.HAL_ReadI2CDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ReadI2C"), typeof(Base.HALI2C.HAL_ReadI2CDelegate));

Base.HALI2C.HAL_CloseI2C = (Base.HALI2C.HAL_CloseI2CDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_CloseI2C"), typeof(Base.HALI2C.HAL_CloseI2CDelegate));
}
}
}

