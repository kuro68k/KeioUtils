# KeioUtils
C# utility function collection. GPLv3.

* __CmdArgs__  
  Handle command line arguments.  
  Options can be specified by '/' or '-'. Options can have multiple names. Option parameters can be separated by a space or '='. Multiple data types supported. Anonymous arguments. Automatic help text generation.

* __IntelHEX__  
  Create/decode Intel HEX files.  
  Mainly of use with embedded applications where Intel HEX is commonly used for firmware images.

* __SIUnits__  
  Convert to and from SI units (e.g. 1.2k = 1200, 2u = 2e-6).  
  Supports 'margin', which defaults to 1.2. On live displays you are often working near round numbers, and don't want the units to keep changhing. For example, you don't want to flip between 999 and 1k constantly, and a 1.2 margin will allow up to 1199 before changinng to 1.2k.

* __BinaryUnits__  
  Convert to and from binary units (1K = 1024 etc.)  
  JEDEC standard.

* __TextUtils__  
  Various text handling utilities.  
  Mostly stuff for handling C format numbers (0b1010, 0x1F etc.).

* __CRC__  
  Calculate CRC16 (CCITT) and CRC32 values.  
  Static methods are available for operating on byte buffers and streams.  
  To process byte-by-byte create a CRC object and use the reset and load methods. For CRC16 use the last value returned by crc16_load(), for CRC32 you must call crc32_read() to get the value.
