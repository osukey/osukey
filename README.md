[//]: # (<p align="center">)

[//]: # (  <img width="500" alt="osu! logo" src="assets/lazer.png">)

[//]: # (</p>)

# osukey(temporary)

[//]: # ([![Build status]&#40;https://github.com/ppy/osu/actions/workflows/ci.yml/badge.svg?branch=master&event=push&#41;]&#40;https://github.com/ppy/osu/actions/workflows/ci.yml&#41;)

[//]: # ([![GitHub release]&#40;https://img.shields.io/github/release/ppy/osu.svg&#41;]&#40;https://github.com/ppy/osu/releases/latest&#41;)

[//]: # ([![CodeFactor]&#40;https://www.codefactor.io/repository/github/ppy/osu/badge&#41;]&#40;https://www.codefactor.io/repository/github/ppy/osu&#41;)

[//]: # ([![dev chat]&#40;https://discordapp.com/api/guilds/188630481301012481/widget.png?style=shield&#41;]&#40;https://discord.gg/ppy&#41;)

[//]: # ([![Crowdin]&#40;https://d322cqt584bo4o.cloudfront.net/osu-web/localized.svg&#41;]&#40;https://crowdin.com/project/osu-web&#41;)

[//]: # (A free-to-win rhythm game. Rhythm is just a *click* away!)

[//]: # ()
[//]: # (The future of [osu!]&#40;https://osu.ppy.sh&#41; and the beginning of an open era! Currently known by and released under the release codename "*lazer*". As in sharper than cutting-edge.)

[//]: # ()
[//]: # (## Status)

[//]: # ()
[//]: # (This project is under heavy development, but is in a stable state. Users are encouraged to try it out and keep it installed alongside the stable *osu!* client. It will continue to evolve to the point of eventually replacing the existing stable client as an update.)

[//]: # ()
[//]: # (**IMPORTANT:** Gameplay mechanics &#40;and other features which you may have come to know and love&#41; are in a constant state of flux. Game balance and final quality-of-life passes come at the end of development, preceded by experimentation and changes which may potentially **reduce playability or usability**. This is done in order to allow us to move forward as developers and designers more efficiently. If this offends you, please consider sticking to the stable releases of osu! &#40;found on the website&#41;. We are not yet open to heated discussion over game mechanics and will not be using github as a forum for such discussions just yet.)

[//]: # ()
[//]: # (We are accepting bug reports &#40;please report with as much detail as possible and follow the existing issue templates&#41;. Feature requests are also welcome, but understand that our focus is on completing the game to feature parity before adding new features. A few resources are available as starting points to getting involved and understanding the project:)

[//]: # ()
[//]: # (- Detailed release changelogs are available on the [official osu! site]&#40;https://osu.ppy.sh/home/changelog/lazer&#41;.)

[//]: # (- You can learn more about our approach to [project management]&#40;https://github.com/ppy/osu/wiki/Project-management&#41;.)

[//]: # (- Read peppy's [blog post]&#40;https://blog.ppy.sh/a-definitive-lazer-faq/&#41; exploring where the project is currently and the roadmap going forward.)

[//]: # ()
[//]: # (## Running osu!)

[//]: # ()
[//]: # (If you are looking to install or test osu! without setting up a development environment, you can consume our [binary releases]&#40;https://github.com/ppy/osu/releases&#41;. Handy links below will download the latest version for your operating system of choice:)

[//]: # ()
[//]: # (**Latest build:**)

[//]: # ()
[//]: # (| [Windows 8.1+ &#40;x64&#41;]&#40;https://github.com/ppy/osu/releases/latest/download/install.exe&#41; | macOS 10.15+ &#40;[Intel]&#40;https://github.com/ppy/osu/releases/latest/download/osu.app.Intel.zip&#41;, [Apple Silicon]&#40;https://github.com/ppy/osu/releases/latest/download/osu.app.Apple.Silicon.zip&#41;&#41; | [Linux &#40;x64&#41;]&#40;https://github.com/ppy/osu/releases/latest/download/osu.AppImage&#41; | [iOS 13.4+]&#40;https://osu.ppy.sh/home/testflight&#41; | [Android 5+]&#40;https://github.com/ppy/osu/releases/latest/download/sh.ppy.osulazer.apk&#41; |)

[//]: # (| ------------- | ------------- | ------------- | ------------- | ------------- |)

[//]: # ()
[//]: # (- The iOS testflight link may fill up &#40;Apple has a hard limit of 10,000 users&#41;. We reset it occasionally when this happens. Please do not ask about this. Check back regularly for link resets or follow [peppy]&#40;https://twitter.com/ppy&#41; on twitter for announcements of link resets.)

[//]: # ()
[//]: # (If your platform is not listed above, there is still a chance you can manually build it by following the instructions below.)

A Misskey client based on osu!lazer.

## Goals
- [ ] Misskey v12 API Implementation
- [ ] Misskey's key features implemented
- [ ] Push Notification Implementation
- [ ] Optimization for touch devices

## Developing osukey(temporary)

Please make sure you have the following prerequisites:

- A desktop platform with the [.NET 6.0 SDK](https://dotnet.microsoft.com/download) installed.
- When developing with mobile, [Xamarin](https://docs.microsoft.com/en-us/xamarin/) is required, which is shipped together with Visual Studio or [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/).
- When working with the codebase, we recommend using an IDE with intelligent code completion and syntax highlighting, such as the latest version of [Visual Studio](https://visualstudio.microsoft.com/vs/), [JetBrains Rider](https://www.jetbrains.com/rider/) or [Visual Studio Code](https://code.visualstudio.com/).
- When running on Linux, please have a system-wide FFmpeg installation available to support video decoding.

### Downloading the source code

Clone the repository:

```shell
git clone https://github.com/sim1222/osukey
cd osukey
```

To update the source code to the latest commit, run the following command inside the `osukey` directory:

```shell
git pull
```

### Building

Build configurations for the recommended IDEs (listed above) are included. You should use the provided Build/Run functionality of your IDE to get things going. When testing or building new components, it's highly encouraged you use the `VisualTests` project/configuration. More information on this is provided [below](#contributing).

- Visual Studio / Rider users should load the project via one of the platform-specific `.slnf` files, rather than the main `.sln`. This will allow access to template run configurations.

You can also build and run *osu!* from the command-line with a single command:

```shell
dotnet run --project osu.Desktop
```

If you are not interested in debugging *osu!*, you can add `-c Release` to gain performance. In this case, you must replace `Debug` with `Release` in any commands mentioned in this document.

If the build fails, try to restore NuGet packages with `dotnet restore`.

_Due to a historical feature gap between .NET Core and Xamarin, running `dotnet` CLI from the root directory will not work for most commands. This can be resolved by specifying a target `.csproj` or the helper project at `build/Desktop.proj`. Configurations have been provided to work around this issue for all supported IDEs mentioned above._

### Testing with resource/framework modifications

Sometimes it may be necessary to cross-test changes in [osu-resources](https://github.com/ppy/osu-resources) or [osu-framework](https://github.com/ppy/osu-framework). This can be achieved by running some commands as documented on the [osu-resources](https://github.com/ppy/osu-resources/wiki/Testing-local-resources-checkout-with-other-projects) and [osu-framework](https://github.com/ppy/osu-framework/wiki/Testing-local-framework-checkout-with-other-projects) wiki pages.

### Code analysis

Before committing your code, please run a code formatter. This can be achieved by running `dotnet format` in the command line, or using the `Format code` command in your IDE.

We have adopted some cross-platform, compiler integrated analyzers. They can provide warnings when you are editing, building inside IDE or from command line, as-if they are provided by the compiler itself.

JetBrains ReSharper InspectCode is also used for wider rule sets. You can run it from PowerShell with `.\InspectCode.ps1`. Alternatively, you can install ReSharper or use Rider to get inline support in your IDE of choice.

<!--
## Contributing

When it comes to contributing to the project, the two main things you can do to help out are reporting issues and submitting pull requests. Please refer to the [contributing guidelines](CONTRIBUTING.md) to understand how to help in the most effective way possible.

If you wish to help with localisation efforts, head over to [crowdin](https://crowdin.com/project/osu-web).

For those interested, we love to reward quality contributions via [bounties](https://docs.google.com/spreadsheets/d/1jNXfj_S3Pb5PErA-czDdC9DUu4IgUbe1Lt8E7CYUJuE/view?&rm=minimal#gid=523803337), paid out via PayPal or osu!supporter tags. Don't hesitate to [request a bounty](https://docs.google.com/forms/d/e/1FAIpQLSet_8iFAgPMG526pBZ2Kic6HSh7XPM3fE8xPcnWNkMzINDdYg/viewform) for your work on this project.
-->

## Licence

*osu!*'s code and framework are licensed under the [MIT licence](https://opensource.org/licenses/MIT). Please see [the licence file](LICENCE) for more information. [tl;dr](https://tldrlegal.com/license/mit-license) you can do whatever you want as long as you include the original copyright and license notice in any copy of the software/source.

Please note that this *does not cover* the usage of the "osu!" or "ppy" branding in any software, resources, advertising or promotion, as this is protected by trademark law.

Please also note that game resources are covered by a separate licence. Please see the [ppy/osu-resources](https://github.com/ppy/osu-resources) repository for clarifications.
