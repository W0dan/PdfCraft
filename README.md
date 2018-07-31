PdfCraft
========

A library to create PDF's

The library is meant to provide a means to generate PDF-files without file-access and as fast as possible, while
keeping the API developer-friendly. Given these constraints, I tried to put as many features in it as possible.
However, it is a work in progress and not finished by far.

For examples on how to use this library, I'd like to refer to the (unit-)tests, especially those under 'API'.

The unit tests can drop their output in a file by uncommenting or adding a call to the method 'DumpToRandomFile()'.
Most tests don't write to the filesystem. I tend to dump the output only for the test I'm currently working on.

Currently on the TODO-list:
- improve support for images,
- improve support for TrueType fonts (a TrueType font that works is included as embedded resource in the tests).



[obligatory disclaimer]
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
[/obligatory disclaimer]
