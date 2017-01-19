# KeioUtils
C# utility function collection

* __CmdArgs__  
  Handle command line arguments.  
  Options can be specified by '/' or '-'. Options can have multiple names. Multiple data types supported.

* __IntelHEX__  
  Create/decode Intel HEX files.  
  Mainly of use with embedded applications where Intel HEX is commonly used for firmware images.

* __SIUnits__  
  Convert to and from SI units (e.g. 1.2k = 1200, 2u = 2e-6).  
  Supports 'margin', which defaults to 1.2. On live displays you are often working near round numbers, and don't want the units to keep changhing. For example, you don't want to flip between 999 and 1k constantly, and a 1.2 margin will allow up to 1200 before changinng to 1.2k.

* __TextUtils__  
  Various text handling utilities.  
  Mostly stuff for handling C format numbers (0b1010, 0x1F etc.).
