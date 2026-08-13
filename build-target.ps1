param($target)

New-Item -Force -ItemType directory -Path src/runtimes/$target/native
New-Item -Force -ItemType directory -Path src/static/$target

# Install compiler if not already present
switch ($target)
{
  "linux-arm"   { sudo apt-get install -y gcc-arm-linux-gnueabihf }
  "linux-arm64" {  }
  "linux-x64"   {  }
  "linux-x86"   { sudo apt-get install -y gcc-i686-linux-gnu }
  "win-arm64"   {  }
  "win-x64"     {  }
  "win-x86"     {  }
  "osx-arm64"   {  }
  "osx-x64"     {  }
}

# Build
switch ($target)
{
  "linux-arm"
  {
    arm-linux-gnueabihf-gcc -c temp/stb_vorbis.c -o src/static/$target/libstbvorbis.o -O2 -fPIC
    arm-linux-gnueabihf-ar rcs src/static/$target/libstbvorbis.a src/static/$target/libstbvorbis.o
    arm-linux-gnueabihf-gcc -shared -o src/runtimes/$target/native/libstbvorbis.so temp/stb_vorbis.c -O2
    Remove-Item "src/static/$target/libstbvorbis.o"
  }
  "linux-arm64"
  {
    aarch64-linux-gnu-gcc -c temp/stb_vorbis.c -o src/static/$target/libstbvorbis.o -O2 -fPIC
    aarch64-linux-gnu-ar rcs src/static/$target/libstbvorbis.a src/static/$target/libstbvorbis.o
    aarch64-linux-gnu-gcc -shared -o src/runtimes/$target/native/libstbvorbis.so temp/stb_vorbis.c -O2
    Remove-Item "src/static/$target/libstbvorbis.o"
  }
  "linux-x64"
  {
    x86_64-linux-gnu-gcc -c temp/stb_vorbis.c -o src/static/$target/libstbvorbis.o -O2 -fPIC
    x86_64-linux-gnu-ar rcs src/static/$target/libstbvorbis.a src/static/$target/libstbvorbis.o
    x86_64-linux-gnu-gcc -shared -o src/runtimes/$target/native/libstbvorbis.so temp/stb_vorbis.c -O2
    Remove-Item "src/static/$target/libstbvorbis.o"
  }
  "linux-x86"
  {
    i686-linux-gnu-gcc -c temp/stb_vorbis.c -o src/static/$target/libstbvorbis.o -O2 -fPIC
    i686-linux-gnu-ar rcs src/static/$target/libstbvorbis.a src/static/$target/libstbvorbis.o
    i686-linux-gnu-gcc -shared -o src/runtimes/$target/native/libstbvorbis.so temp/stb_vorbis.c -O2
    Remove-Item "src/static/$target/libstbvorbis.o"
  }
  "osx-arm64"
  {
    clang -target arm64-apple-darwin -c temp/stb_vorbis.c -o src/static/$target/libstbvorbis.o -O2
    ar rcs src/static/$target/libstbvorbis.a src/static/$target/libstbvorbis.o
    clang -target arm64-apple-darwin -shared -o src/runtimes/$target/native/libstbvorbis.dylib temp/stb_vorbis.c -O2
    Remove-Item "src/static/$target/libstbvorbis.o"
  }
  "osx-x64"
  {
    clang -target x86_64-apple-darwin -c temp/stb_vorbis.c -o src/static/$target/libstbvorbis.o -O2
    ar rcs src/static/$target/libstbvorbis.a src/static/$target/libstbvorbis.o
    clang -target x86_64-apple-darwin -shared -o src/runtimes/$target/native/libstbvorbis.dylib temp/stb_vorbis.c -O2
    Remove-Item "src/static/$target/libstbvorbis.o"
  }
  {$_ -like "win-*"}
  {
    $vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
    $vsPath = & $vswhere -latest -products * -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64 -property installationPath
    $vcVarsPath = "$vsPath\VC\Auxiliary\Build\vcvarsall.bat"
    $arch = $target -replace 'win-', ''
    
    $cmd =
      ("`"$vcVarsPath`" $arch && ") +
      ("cl /c /O2 temp\stb_vorbis.c /Fo:src\static\$target\libstbvorbis.obj && ") +
      ("lib src\static\$target\libstbvorbis.obj /OUT:src\static\$target\libstbvorbis.lib && ") +
      ("cl /LD /O2 temp\stb_vorbis.c /link /OUT:src\runtimes\$target\native\libstbvorbis.dll")

    cmd.exe /c $cmd
    Remove-Item "src\static\$target\libstbvorbis.obj"
  }
}