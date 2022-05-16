# `bus-homework` repository

Greetings! This git repository represents a "homework assignment" to implement a system modelling the basic functionality for a bus/transit operation.

The repo is consists of:

* `git`-related components
* This `README.md` file
* A `docker-compose.yml` file to reify the runtime system, see _Running The System_ below
* several top-level directories that represent discrete sub-sections of the total solution; details below

## Running The System

0. Complete the _Environmental Requirements_ section below
1. You can bring up the actual system to use live or regenerate living docs against by running `docker-compose up --detach`, after which the runtime components can be evaluated
2. To use the site, point your local browser at `http://bus-homework.example` (assuming you are browsing on the same machine that is running, if not then some `/etc/hosts` entries (or Windows analog) may be needed)
3. Optionally at any time a live system is up, if you want to regenerate the [SpecFlow-generated LivingDoc.html file](https://specflow.org/tools/living-doc/), run the `regen-living-docs.[sh|ps1]` file (preferrably from within a shell that is `cd`'d into the `/specs` folder of this repository; the powershell version will spawn a chrome browser)

Enjoy!

## Environmental Requirements

This `git` repo should be cloned onto a development machine with the following installed/available/completed/etc:

* Docker Desktop or other apropriate dependencies to run `docker-compose` installed to handle the runtime system
* `dotnet` 6 sdk installed to run the `specs` component
* `node`, v16 LTS installed, `npm` and `npx` installed to live debug/modify the `webapp` component
* VS code available (optimally, but not neccesary) for any programming, debugging, etc
* To generate SpecFlow BDD Living Docs (explained below), the `Specflow.Plus.LivingDoc.CLI` tool must be installed as per [the SpecFlow docs](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Installing-the-command-line-tool.html)
* Ability to run either *NIX shell script or Windows powershell to run `regen-living-docs.[sh|ps1]` script
* The ability to edit your local /etc/hosts file (or aprop version of this on Windows) to map the hostname `bus-homework.example` to `127.0.0.1`
  - This is done to simplify the CORS story and present an example of a gateway'd, containized solution that can transition to Kubernetes hosting in the future
  - the webapp is designed to talk to a backend on the same hostname space
  - the `docker-compose.yml` is laid out in such a way that only the `gateway` component exposes public ports, and it will bind to port `80` on `localhost`; it is designed to route requests to the `bus-homework.example` hostname

If the above steps have conflicts with existing machine setup or aren't available, then most of this should be able to be brought up in a linux VM that installs the above deps (and mainly be able to powerful enough to run a desktop for a browser + docker + vs code arrangement if you want to dev)

## Sub-systems

* `api` - A `dotnet` sln root with a single HTTP project in `src/BusHomework.Api`; main entry-point is via a `Dockerfile` utilized in the top-level `docker-compose.yml`; responds to requests from the `webapp` client
* `webapp` - a React+Redux+MUI webapp (created by `create-react-app` with the `redux-typescript` template), delivered at runtime by a separate `nginx` instance serving the artifacts of a docker-based build process; depends on `api` at runtime
* `gateway` - an `nginx` frontend, serves up access to `api` + `webapp` and depends on them
- `selenium-runner` a simple linux+firefox container that can act as a receiver of `WebDriver`-based selenium sessions, or as part of a larger selenium grid (if configured to do so)
  - For this repo, the container serves to support the `specs` component, which uses the Selenium.NET bindings to tests that run on `selenium-runner` and get the results back (and report them as part of the living docs)
* `specs` - A `dotnet` sln root with a single project in the `src/BusHomework.Spec` directory
  - TL;DR - SpecFlow-based BDD-style specs with [Living Documentation](https://specflow.org/tools/living-doc/); firewalled from the actual implementation of `bus-homework`, but paradoxically the place where all work against the system should originate
  - within the `Features` directory of this project are plain-language Gherkin-style `.feature` files covering tests that target `api` and `webapp` as black-box systems, tested in a full-integration context
  - running the `regen-living-docs.[sh|ps1]` script will build the project and run the BDD specs 
  - More about the SpecFlow.LivingDocs tool, etc can be learned at [the SpecFlow web site](https://specflow.org)
  - Web tests are ran via the `selenium-runner` component; straight-up API calls are done from `dotnet` using `HTTPClient`
  - Depending on how much participation you can get (ie can you get a product owner or other functional roles to install VS Code/Visual Studio 2022 + git etc and run this), you are presented with a self-documenting implementation that:
    - Provides a pleasant and interactive way to do Test-First Development (Gherkin Features are much more expressive; bridges a gap between code-based Unit Test Frameworks and an Automated Acceptance Testing tool like [FitNesse](http://fitnesse.org/FitNesse.UserGuide.AcceptanceTests))
    - Naturally captures what would otherwise live in the interior of Stories, Acceptance Criteria, etc in any software-based project management tool, that lives under source control (with all the benefits and drawbacks); post-adoption stories/cards would just reference Features and maybe Scenarios but otherwise be shells, with actual information in the Features/Scenarios
    - Interesting fact: Features and Scenarios DO NOT have to be backed by code (ie leave out any Steps, Rules, etc) in order to contain meaningful documentation (e.g. Test Cases) that can be used to re-document/unify documentation for legacy systems (in this case legacy means "not developed under a SpecFlow/BDD/LivingDoc regime"); in this arrangement the Scenario-content just becomes doc strings with instructions for test process, etc

## Missing functionality, nice to haves, etc

* The first thing I would add if this wasn't a time-constained POC would be authn/authz access control; `keycloak` is an obvious OSS solution supported by a major vendor; Okta, Auth0, etc would be SaaS vendor options, some reasons to avoid this:
  - One reason to skip is the homework constraint for no data-persistence component (which an authn/authz system implies)
  - All runtime components need to be updated to handle sessions, tokens, etc
  - It creates the need for an additional layer of test support infrastructure in the specs for token+session management; This is neccesary in any project that would be "production-ready", but is ommited for the sake of brevity
* The second thing would be accessibility concerns, handled more explicitly; for the purposes of this homework the `webapp` is reasonably screen-reader friendly; The way it does updates should be screen reader friendly, as well
* Logging, telemetry, perf metrics, etc
* Timezones, Timezones, Timezones!
  - I majorly punted on this, but I think a solution would involves:
  - Storing all arrival times, for a stop in the backend, in UTC
  - tying stops/routes/etc into a given "transit system" that would have a "native" timezone; this is all relational stuff with regard to the client/customer (transit systems) and where they're based; It seems like something like this would arise naturally in a schema for such a system (assuming multi-tenancy requirements doesn't push this all into separate DB instances)
  - Separate from that, we would want to understand the timezones of the clients using the site[/calling api]; if the client sets/sends TZ in the call, then it can be converted in the backend for transport (or all clients receive time in UTC and then make decisions)
* TLS+SSL; encryption of internal traffic from the `gateway` component to it's internal peers; all stuff that would hopefully be dealt-with within a Kubernetes install
* It would be nice to push the `LivingDoc.html` creation into a docker-based process that emits the file as a side-effect, but otherwise leaves in no side-effects (and makes the `dotnet`/SpecFlow dependencies go away)