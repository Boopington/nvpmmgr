Taken from https://code.google.com/archive/p/nvpmmgr/, imported into visual studio 2019, uploaded here for safekeeping and because I like git over svn.

This program lets you easily edit and reload "PowerMizer" settings. "PowerMizer" mixed with the "PerfLevelSrc" (both set by this program) is the exact same setup that EVGA's KBoost utilizes to prevent the huge clock fluctuations introduced by GPU Boost 3.0. In my experience, disabling any kind of clock fluctuations (including Intel's Speedstep/TurboBoost technology) results in a dramatic reduction in frame time hitches in games.

The disable core downclock sets EnableCoreSlowdown=0, EnableMClkSlowdown=0, and EnableNVClkSlowdown=0. These do not seem to apply to GPU Boost 3.0s temperature downclocking, and you likely don't want to mess with them.  
