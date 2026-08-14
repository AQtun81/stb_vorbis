#ifdef _WIN32
  #define VORBIS_EXPORT __declspec(dllexport)
#else
  #define VORBIS_EXPORT __attribute__((visibility("default")))
#endif

#define STB_VORBIS_NO_STDIO
