$functionsToExport = @(
    'stb_vorbis_get_info',
    'stb_vorbis_get_comment',
    'stb_vorbis_get_error',
    'stb_vorbis_close',
    'stb_vorbis_get_sample_offset',
    'stb_vorbis_get_file_offset',
    'stb_vorbis_open_pushdata',
    'stb_vorbis_decode_frame_pushdata',
    'stb_vorbis_flush_pushdata',
    'stb_vorbis_decode_filename',
    'stb_vorbis_decode_memory',
    'stb_vorbis_open_memory',
    'stb_vorbis_open_filename',
    'stb_vorbis_open_file',
    'stb_vorbis_open_file_section',
    'stb_vorbis_seek_frame',
    'stb_vorbis_seek',
    'stb_vorbis_seek_start',
    'stb_vorbis_stream_length_in_samples',
    'stb_vorbis_stream_length_in_seconds',
    'stb_vorbis_get_frame_float',
    'stb_vorbis_get_frame_short_interleaved',
    'stb_vorbis_get_frame_short',
    'stb_vorbis_get_samples_float_interleaved',
    'stb_vorbis_get_samples_float',
    'stb_vorbis_get_samples_short_interleaved',
    'stb_vorbis_get_samples_short'
)
$pattern = '\b(' + ($functionsToExport -join '|') + ')\b'

New-Item -Force -ItemType directory -Path temp | Out-Null

(Get-Content -Path "stb_vorbis_macro.h") | Out-File -FilePath "temp/stb_vorbis.c" -Encoding UTF8
foreach ($line in Get-Content -Path "stb/stb_vorbis.c") {
    if ($line -match $pattern -and $line.Trim().StartsWith("extern")) {
        $line = 'VORBIS_EXPORT ' + $line
    } 
    $line | Out-File -FilePath "temp/stb_vorbis.c" -Encoding UTF8 -Append
}