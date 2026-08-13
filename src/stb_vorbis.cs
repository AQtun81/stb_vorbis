using System;
using System.Runtime.InteropServices;

namespace AQtun.stb.vorbis
{
    /// <summary>
    /// decode ogg vorbis files from file/memory to float/16-bit signed output
    /// </summary>
    public static unsafe partial class stb_vorbis
    {
        #if NET5_0_OR_GREATER || NETCOREAPP || NETSTANDARD
        private const string NATIVE_LIB = "libstbvorbis";
        #else // NETFRAMEWORK
        private const string NATIVE_LIB = "libstbvorbis.dll";
        #endif

        /// <summary>
        /// get general information about the file
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_get_info")]
        public static partial stb_vorbis_info get_info(stb_vorbis_ptr* file);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_get_info")]
        public static extern stb_vorbis_info get_info(stb_vorbis_ptr* file);
        #endif

// todo implement stb_vorbis_get_comment
/*         #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_get_comment")]
        public static partial int get_comment(IntPtr todoIncompleteFunctionCall);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_get_comment")]
        public static extern int get_comment(IntPtr todoIncompleteFunctionCall);
        #endif */

        /// <summary>
        /// get the last error detected (clears it, too)
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_get_error")]
        public static partial int get_error(stb_vorbis_ptr* file);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_get_error")]
        public static extern int get_error(stb_vorbis_ptr* file);
        #endif

        /// <summary>
        /// close an ogg vorbis file and free all memory in use
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_close")]
        public static partial void close(stb_vorbis_ptr* file);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_close")]
        public static extern void close(stb_vorbis_ptr* file);
        #endif

        /// <summary>
        /// this function returns the offset (in samples) from the beginning of the
        /// file that will be returned by the next decode, if it is known, or -1
        /// otherwise. after a flush_pushdata() call, this may take a while before
        /// it becomes valid again.
        /// NOT WORKING YET after a seek with PULLDATA API
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_get_sample_offset")]
        public static partial int get_sample_offset(stb_vorbis_ptr* file);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_get_sample_offset")]
        public static extern int get_sample_offset(stb_vorbis_ptr* file);
        #endif

        /// <summary>
        /// returns the current seek point within the file, or offset from the beginning
        /// of the memory buffer. In pushdata mode it returns 0.
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_get_file_offset")]
        public static partial uint get_file_offset(stb_vorbis_ptr* file);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_get_file_offset")]
        public static extern uint get_file_offset(stb_vorbis_ptr* file);
        #endif

        /// <summary>
        /// create a vorbis decoder by passing in the initial data block containing
        ///    the oggvorbis headers (you don't need to do parse them, just provide
        ///    the first N bytes of the file--you're told if it's not enough, see below)
        /// on success, returns an stb_vorbis *, does not set error, returns the amount of
        ///    data parsed/consumed on this call in *datablock_memory_consumed_in_bytes;
        /// on failure, returns NULL on error and sets *error, does not change *datablock_memory_consumed
        /// if returns NULL and *error is VORBIS_need_more_data, then the input block was
        ///       incomplete and you need to pass in a larger block from the start of the file
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_open_pushdata")]
        public static partial stb_vorbis_ptr* open_pushdata(byte* datablock, int datablock_length_in_bytes, ref int datablock_memory_consumed_in_bytes, ref int error, stb_vorbis_alloc* alloc_buffer = null);
        
        /// <summary>
        /// create a vorbis decoder by passing in the initial data block containing
        ///    the oggvorbis headers (you don't need to do parse them, just provide
        ///    the first N bytes of the file--you're told if it's not enough, see below)
        /// on success, returns an stb_vorbis *, does not set error, returns the amount of
        ///    data parsed/consumed on this call in *datablock_memory_consumed_in_bytes;
        /// on failure, returns NULL on error and sets *error, does not change *datablock_memory_consumed
        /// if returns NULL and *error is VORBIS_need_more_data, then the input block was
        ///       incomplete and you need to pass in a larger block from the start of the file
        /// </summary>
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_open_pushdata")]
        public static partial stb_vorbis_ptr* open_pushdata(byte* datablock, int datablock_length_in_bytes, int* datablock_memory_consumed_in_bytes, int* error, stb_vorbis_alloc* alloc_buffer = null);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_open_pushdata")]
        public static extern stb_vorbis_ptr* open_pushdata(byte* datablock, int datablock_length_in_bytes, int* datablock_memory_consumed_in_bytes, int* error, stb_vorbis_alloc* alloc_buffer = null);
        #endif

        /// <summary>
        /// decode a frame of audio sample data if possible from the passed-in data block
        ///
        /// return value: number of bytes we used from datablock
        ///
        /// possible cases:
        ///     0 bytes used, 0 samples output (need more data)
        ///     N bytes used, 0 samples output (resynching the stream, keep going)
        ///     N bytes used, M samples output (one frame of data)
        /// note that after opening a file, you will ALWAYS get one N-bytes,0-sample
        /// frame, because Vorbis always "discards" the first frame.
        ///
        /// Note that on resynch, stb_vorbis will rarely consume all of the buffer,
        /// instead only datablock_length_in_bytes-3 or less. This is because it wants
        /// to avoid missing parts of a page header if they cross a datablock boundary,
        /// without writing state-machiney code to record a partial detection.
        ///
        /// The number of channels returned are stored in *channels (which can be
        /// NULL--it is always the same as the number of channels reported by
        /// get_info). *output will contain an array of float* buffers, one per
        /// channel. In other words, (*output)[0][0] contains the first sample from
        /// the first channel, and (*output)[1][0] contains the first sample from
        /// the second channel.
        ///
        /// *output points into stb_vorbis's internal output buffer storage; these
        /// buffers are owned by stb_vorbis and application code should not free
        /// them or modify their contents. They are transient and will be overwritten
        /// once you ask for more data to get decoded, so be sure to grab any data
        /// you need before then.
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_decode_frame_pushdata")]
        public static partial int decode_frame_pushdata(stb_vorbis_ptr* file, byte* datablock, int datablock_length_in_bytes, ref int channels, ref float** output, ref int samples);

        /// <summary>
        /// decode a frame of audio sample data if possible from the passed-in data block
        ///
        /// return value: number of bytes we used from datablock
        ///
        /// possible cases:
        ///     0 bytes used, 0 samples output (need more data)
        ///     N bytes used, 0 samples output (resynching the stream, keep going)
        ///     N bytes used, M samples output (one frame of data)
        /// note that after opening a file, you will ALWAYS get one N-bytes,0-sample
        /// frame, because Vorbis always "discards" the first frame.
        ///
        /// Note that on resynch, stb_vorbis will rarely consume all of the buffer,
        /// instead only datablock_length_in_bytes-3 or less. This is because it wants
        /// to avoid missing parts of a page header if they cross a datablock boundary,
        /// without writing state-machiney code to record a partial detection.
        ///
        /// The number of channels returned are stored in *channels (which can be
        /// NULL--it is always the same as the number of channels reported by
        /// get_info). *output will contain an array of float* buffers, one per
        /// channel. In other words, (*output)[0][0] contains the first sample from
        /// the first channel, and (*output)[1][0] contains the first sample from
        /// the second channel.
        ///
        /// *output points into stb_vorbis's internal output buffer storage; these
        /// buffers are owned by stb_vorbis and application code should not free
        /// them or modify their contents. They are transient and will be overwritten
        /// once you ask for more data to get decoded, so be sure to grab any data
        /// you need before then.
        /// </summary>
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_decode_frame_pushdata")]
        public static partial int decode_frame_pushdata(stb_vorbis_ptr* file, byte* datablock, int datablock_length_in_bytes, int* channels, float*** output, int* samples);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_decode_frame_pushdata")]
        public static extern int decode_frame_pushdata(stb_vorbis_ptr* file, byte* datablock, int datablock_length_in_bytes, int* channels, float*** output, int* samples);
        #endif

        /// <summary>
        /// inform stb_vorbis that your next datablock will not be contiguous with
        /// previous ones (e.g. you've seeked in the data); future attempts to decode
        /// frames will cause stb_vorbis to resynchronize (as noted above), and
        /// once it sees a valid Ogg page (typically 4-8KB, as large as 64KB), it
        /// will begin decoding the _next_ frame.
        ///
        /// if you want to seek using pushdata, you need to seek in your file, then
        /// call stb_vorbis_flush_pushdata(), then start calling decoding, then once
        /// decoding is returning you data, call stb_vorbis_get_sample_offset, and
        /// if you don't like the result, seek your file again and repeat.
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_flush_pushdata")]
        public static partial void flush_pushdata(stb_vorbis_ptr* file);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_flush_pushdata")]
        public static extern void flush_pushdata(stb_vorbis_ptr* file);
        #endif

        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_decode_filename")]
        public static partial int decode_filename([MarshalAs(UnmanagedType.LPStr)] string filename, ref int channels, ref int sample_rate, ref short* output);
        
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_decode_filename")]
        public static partial int decode_filename(byte* filename, int* channels, int* sample_rate, short** output);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_decode_filename")]
        public static extern int decode_filename(byte* filename, int* channels, int* sample_rate, short** output);
        #endif

        /// <summary>
        /// decode an entire file and output the data interleaved into a malloc()ed
        /// buffer stored in *output. The return value is the number of samples
        /// decoded, or -1 if the file could not be opened or was not an ogg vorbis file.
        /// When you're done with it, just free() the pointer returned in *output.
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_decode_memory")]
        public static partial int decode_memory(byte* mem, int len, ref int channels, ref int sample_rate, ref short* output);

        /// <summary>
        /// decode an entire file and output the data interleaved into a malloc()ed
        /// buffer stored in *output. The return value is the number of samples
        /// decoded, or -1 if the file could not be opened or was not an ogg vorbis file.
        /// When you're done with it, just free() the pointer returned in *output.
        /// </summary>
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_decode_memory")]
        public static partial int decode_memory(byte* mem, int len, int* channels, int* sample_rate, short** output);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_decode_memory")]
        public static extern int decode_memory(byte* mem, int len, int* channels, int* sample_rate, short** output);
        #endif

        /// <summary>
        /// create an ogg vorbis decoder from an ogg vorbis stream in memory (note
        /// this must be the entire stream!). on failure, returns NULL and sets *error
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_open_memory")]
        public static partial stb_vorbis_ptr* open_memory(byte* data, int len, ref int error, stb_vorbis_alloc* alloc_buffer = null);
        
        /// <summary>
        /// create an ogg vorbis decoder from an ogg vorbis stream in memory (note
        /// this must be the entire stream!). on failure, returns NULL and sets *error
        /// </summary>
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_open_memory")]
        public static partial stb_vorbis_ptr* open_memory(byte* data, int len, int* error, stb_vorbis_alloc* alloc_buffer = null);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_open_memory")]
        public static extern stb_vorbis_ptr* open_memory(byte* data, int len, int* error, stb_vorbis_alloc* alloc_buffer = null);
        #endif

        /// <summary>
        /// create an ogg vorbis decoder from a filename via fopen(). on failure,
        /// returns NULL and sets *error (possibly to VORBIS_file_open_failure).
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_open_filename")]
        public static partial stb_vorbis_ptr* open_filename([MarshalAs(UnmanagedType.LPStr)] string filename, ref int error, stb_vorbis_alloc* alloc_buffer = null);

        /// <summary>
        /// create an ogg vorbis decoder from a filename via fopen(). on failure,
        /// returns NULL and sets *error (possibly to VORBIS_file_open_failure).
        /// </summary>
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_open_filename")]
        public static partial stb_vorbis_ptr* open_filename(byte* filename, int* error, stb_vorbis_alloc* alloc_buffer = null);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_open_filename")]
        public static extern stb_vorbis_ptr* open_filename([MarshalAs(UnmanagedType.LPStr)] string filename, int* error, stb_vorbis_alloc* alloc_buffer = null);

        /// <summary>
        /// create an ogg vorbis decoder from a filename via fopen(). on failure,
        /// returns NULL and sets *error (possibly to VORBIS_file_open_failure).
        /// </summary>
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_open_filename")]
        public static extern stb_vorbis_ptr* open_filename(byte* filename, int* error, stb_vorbis_alloc* alloc_buffer = null);
        #endif

// todo implement stb_vorbis_open_file and stb_vorbis_open_file_section
/*         #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_open_file")]
        public static partial int open_file(IntPtr todoIncompleteFunctionCall);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_open_file")]
        public static extern int open_file(IntPtr todoIncompleteFunctionCall);
        #endif

        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_open_file_section")]
        public static partial int open_file_section(IntPtr todoIncompleteFunctionCall);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_open_file_section")]
        public static extern int open_file_section(IntPtr todoIncompleteFunctionCall);
        #endif */

        /// <summary>
        /// these functions seek in the Vorbis file to (approximately) 'sample_number'.
        /// after calling seek_frame(), the next call to get_frame_*() will include
        /// the specified sample. after calling stb_vorbis_seek(), the next call to
        /// stb_vorbis_get_samples_* will start with the specified sample. If you
        /// do not need to seek to EXACTLY the target sample when using get_samples_*,
        /// you can also use seek_frame().
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_seek_frame")]
        public static partial int seek_frame(stb_vorbis_ptr* file, uint sample_number);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_seek_frame")]
        public static extern int seek_frame(stb_vorbis_ptr* file, uint sample_number);
        #endif

        /// <summary>
        /// these functions seek in the Vorbis file to (approximately) 'sample_number'.
        /// after calling seek_frame(), the next call to get_frame_*() will include
        /// the specified sample. after calling stb_vorbis_seek(), the next call to
        /// stb_vorbis_get_samples_* will start with the specified sample. If you
        /// do not need to seek to EXACTLY the target sample when using get_samples_*,
        /// you can also use seek_frame().
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_seek")]
        public static partial int seek(stb_vorbis_ptr* file, uint sample_number);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_seek")]
        public static extern int seek(stb_vorbis_ptr* file, uint sample_number);
        #endif

        /// <summary>
        /// this function is equivalent to stb_vorbis_seek(f,0)
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_seek_start")]
        public static partial int seek_start(stb_vorbis_ptr* file);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_seek_start")]
        public static extern int seek_start(stb_vorbis_ptr* file);
        #endif

        /// <summary>
        /// these functions return the total length of the vorbis stream
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_stream_length_in_samples")]
        public static partial uint stream_length_in_samples(stb_vorbis_ptr* file);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_stream_length_in_samples")]
        public static extern uint stream_length_in_samples(stb_vorbis_ptr* file);
        #endif

        /// <summary>
        /// these functions return the total length of the vorbis stream
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_stream_length_in_seconds")]
        public static partial float stream_length_in_seconds(stb_vorbis_ptr* file);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_stream_length_in_seconds")]
        public static extern float stream_length_in_seconds(stb_vorbis_ptr* file);
        #endif

        /// <summary>
        /// decode the next frame and return the number of samples. the number of
        /// channels returned are stored in *channels (which can be NULL--it is always
        /// the same as the number of channels reported by get_info). *output will
        /// contain an array of float* buffers, one per channel. These outputs will
        /// be overwritten on the next call to stb_vorbis_get_frame_*.
        ///
        /// You generally should not intermix calls to stb_vorbis_get_frame_*()
        /// and stb_vorbis_get_samples_*(), since the latter calls the former.
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_get_frame_float")]
        public static partial int get_frame_float(stb_vorbis_ptr* file, ref int channels, ref float** output);

        /// <summary>
        /// decode the next frame and return the number of samples. the number of
        /// channels returned are stored in *channels (which can be NULL--it is always
        /// the same as the number of channels reported by get_info). *output will
        /// contain an array of float* buffers, one per channel. These outputs will
        /// be overwritten on the next call to stb_vorbis_get_frame_*.
        ///
        /// You generally should not intermix calls to stb_vorbis_get_frame_*()
        /// and stb_vorbis_get_samples_*(), since the latter calls the former.
        /// </summary>
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_get_frame_float")]
        public static partial int get_frame_float(stb_vorbis_ptr* file, int* channels, float*** output);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_get_frame_float")]
        public static extern int get_frame_float(stb_vorbis_ptr* file, int* channels, float*** output);
        #endif

        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_get_frame_short_interleaved")]
        public static partial int get_frame_short_interleaved(stb_vorbis_ptr* file, int num_c, short* buffer, int num_shorts);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_get_frame_short_interleaved")]
        public static extern int get_frame_short_interleaved(stb_vorbis_ptr* file, int num_c, short* buffer, int num_shorts);
        #endif

        /// <summary>
        /// decode the next frame and return the number of *samples* per channel.
        /// Note that for interleaved data, you pass in the number of shorts (the
        /// size of your array), but the return value is the number of samples per
        /// channel, not the total number of samples.
        ///
        /// The data is coerced to the number of channels you request according to the
        /// channel coercion rules (see below). You must pass in the size of your
        /// buffer(s) so that stb_vorbis will not overwrite the end of the buffer.
        /// The maximum buffer size needed can be gotten from get_info(); however,
        /// the Vorbis I specification implies an absolute maximum of 4096 samples
        /// per channel.
        /// 
        /// Channel coercion rules:
        ///    Let M be the number of channels requested, and N the number of channels present,
        ///    and Cn be the nth channel; let stereo L be the sum of all L and center channels,
        ///    and stereo R be the sum of all R and center channels (channel assignment from the
        ///    vorbis spec).
        ///        M    N       output
        ///        1    k      sum(Ck) for all k
        ///        2    *      stereo L, stereo R
        ///        k    l      k > l, the first l channels, then 0s
        ///        k    l      k <= l, the first k channels
        ///    Note that this is not _good_ surround etc. mixing at all! It's just so
        ///    you get something useful.
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_get_frame_short")]
        public static partial int get_frame_short(stb_vorbis_ptr* file, int num_c, short** buffer, int num_samples);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_get_frame_short")]
        public static extern int get_frame_short(stb_vorbis_ptr* file, int num_c, short** buffer, int num_samples);
        #endif

        /// <summary>
        /// gets num_samples samples, not necessarily on a frame boundary--this requires
        /// buffering so you have to supply the buffers. DOES NOT APPLY THE COERCION RULES.
        /// Returns the number of samples stored per channel; it may be less than requested
        /// at the end of the file. If there are no more samples in the file, returns 0.
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_get_samples_float_interleaved")]
        public static partial int get_samples_float_interleaved(stb_vorbis_ptr* file, int channels, float* buffer, int num_floats);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_get_samples_float_interleaved")]
        public static extern int get_samples_float_interleaved(stb_vorbis_ptr* file, int channels, float* buffer, int num_floats);
        #endif

        /// <summary>
        /// gets num_samples samples, not necessarily on a frame boundary--this requires
        /// buffering so you have to supply the buffers. DOES NOT APPLY THE COERCION RULES.
        /// Returns the number of samples stored per channel; it may be less than requested
        /// at the end of the file. If there are no more samples in the file, returns 0.
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_get_samples_float")]
        public static partial int get_samples_float(stb_vorbis_ptr* file, int channels, float** buffer, int num_samples);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_get_samples_float")]
        public static extern int get_samples_float(stb_vorbis_ptr* file, int channels, float** buffer, int num_samples);
        #endif

        /// <summary>
        /// gets num_samples samples, not necessarily on a frame boundary--this requires
        /// buffering so you have to supply the buffers. Applies the coercion rules above
        /// to produce 'channels' channels. Returns the number of samples stored per channel;
        /// it may be less than requested at the end of the file. If there are no more
        /// samples in the file, returns 0.
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_get_samples_short_interleaved")]
        public static partial int get_samples_short_interleaved(stb_vorbis_ptr* file, int channels, float* buffer, int num_floats);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_get_samples_short_interleaved")]
        public static extern int get_samples_short_interleaved(stb_vorbis_ptr* file, int channels, float* buffer, int num_floats);
        #endif

        /// <summary>
        /// gets num_samples samples, not necessarily on a frame boundary--this requires
        /// buffering so you have to supply the buffers. Applies the coercion rules above
        /// to produce 'channels' channels. Returns the number of samples stored per channel;
        /// it may be less than requested at the end of the file. If there are no more
        /// samples in the file, returns 0.
        /// </summary>
        #if NET7_0_OR_GREATER
        [LibraryImport(NATIVE_LIB, EntryPoint = "stb_vorbis_get_samples_short")]
        public static partial int get_samples_short(stb_vorbis_ptr* file, int channels, float** buffer, int num_samples);
        #else
        [DllImport(NATIVE_LIB, CallingConvention = CallingConvention.Cdecl, EntryPoint = "stb_vorbis_get_samples_short")]
        public static extern int get_samples_short(stb_vorbis_ptr* file, int channels, float** buffer, int num_samples);
        #endif
    }

    public struct stb_vorbis_ptr;

    public struct stb_vorbis_info
    {
        public uint sample_rate;
        public int channels;

        public uint setup_memory_required;
        public uint setup_temp_memory_required;
        public uint temp_memory_required;

        public int max_frame_size;
    }

    public unsafe struct stb_vorbis_alloc
    {
        public void* alloc_buffer;
        public int   alloc_buffer_length_in_bytes;
    }
}