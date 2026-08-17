# cross-platform C# bindings for stb_vorbis

| Platform | x86 | x64 | arm | arm64 |
|----------|-----|-----|-----|-------|
| Windows  | ✓   | ✓   | ✗   | ✓     |
| Linux    | ✓   | ✓   | ✓   | ✓     |
| MacOS    | ✗   | ✓   | ✗   | ✓     |
| Android  | ✗   | ✗   | ✗   | ✗     |
| iOS      | ✗   | ✗   | ✗   | ✗     |

This package bundles both dynamic and static libraries, in AoT builds the library will be statically linked.<br>

The library is compiled with `STB_VORBIS_NO_STDIO`, meaning that `stb_vorbis_open_filename`, `stb_vorbis_open_file`, `stb_vorbis_open_file_section` and `stb_vorbis_decode_filename` are absent.<br>

## Method signatures

```cs
public static stb_vorbis_info get_info(stb_vorbis_handle file);

public static stb_vorbis_comment get_comment(stb_vorbis_handle file);

public static STBVorbisError get_error(stb_vorbis_handle file);

public static void close(stb_vorbis_handle file);

public static int get_sample_offset(stb_vorbis_handle file);

public static uint get_file_offset(stb_vorbis_handle file);

public static stb_vorbis_handle open_pushdata(byte* datablock, int datablock_length_in_bytes, ref int datablock_memory_consumed_in_bytes, out STBVorbisError error, stb_vorbis_alloc* alloc_buffer = null);
public static stb_vorbis_handle open_pushdata(byte* datablock, int datablock_length_in_bytes, int* datablock_memory_consumed_in_bytes, STBVorbisError* error, stb_vorbis_alloc* alloc_buffer = null);

public static int decode_frame_pushdata(stb_vorbis_handle file, byte* datablock, int datablock_length_in_bytes, out int channels, ref float** output, out int samples);
public static int decode_frame_pushdata(stb_vorbis_handle file, byte* datablock, int datablock_length_in_bytes, int* channels, float*** output, int* samples);

public static void flush_pushdata(stb_vorbis_handle file);

public static int decode_memory(byte* mem, int len, out int channels, out int sample_rate, ref short* output);
public static int decode_memory(byte* mem, int len, int* channels, int* sample_rate, short** output);

public static stb_vorbis_handle open_memory(byte* data, int len, out STBVorbisError error, stb_vorbis_alloc* alloc_buffer = null);
public static stb_vorbis_handle open_memory(byte* data, int len, STBVorbisError* error, stb_vorbis_alloc* alloc_buffer = null);

public static int seek_frame(stb_vorbis_handle file, uint sample_number);

public static int seek(stb_vorbis_handle file, uint sample_number);

public static int seek_start(stb_vorbis_handle file);

public static uint stream_length_in_samples(stb_vorbis_handle file);

public static float stream_length_in_seconds(stb_vorbis_handle file);

public static int get_frame_float(stb_vorbis_handle file, out int channels, ref float** output);
public static int get_frame_float(stb_vorbis_handle file, int* channels, float*** output);

public static int get_frame_short_interleaved(stb_vorbis_handle file, int num_c, short* buffer, int num_shorts);

public static int get_frame_short(stb_vorbis_handle file, int num_c, short** buffer, int num_samples);

public static int get_samples_float_interleaved(stb_vorbis_handle file, int channels, float* buffer, int num_floats);

public static int get_samples_float(stb_vorbis_handle file, int channels, float** buffer, int num_samples);

public static int get_samples_short_interleaved(stb_vorbis_handle file, int channels, short* buffer, int num_shorts);

public static int get_samples_short(stb_vorbis_handle file, int channels, short** buffer, int num_samples);
```
