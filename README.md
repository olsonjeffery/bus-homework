# `bus-homework` repository

This is a "homework assignment" to present a system modelling the basic functionality for a bus/transit operation.

The repo is consists of:

* `git`-related components
* This `README.md` file
* A `docker-compose.yml` file to reify the runtime system
  - It *should* be able to be brought up with a `docker-compose up --detach` initial command, after which the `webapp` should be available by visiting `http://bus-homework.example`
* several top-level directories that represent discrete sub-sections of the total solution

## Authorship

Jeffery Olson is the sole author of all of the content held herein; some components (config files, some project patterns and code templates, etc) were imported from other personal projects, but otherwise all code is new for this effort.

## Environmental requirements

This `git` repo should be cloned onto a development machine with the following installed/available/completed/etc:

* Docker Desktop or other apropriate dependencies to run `docker-compose` installed to handle the runtime system
* `dotnet` 6 sdk installed to run the `specs` component
* `node`, v16 LTS installed, `npm` and `npx` installed to live debug/modify the `webapp` component
* VS code available (optimally, but not neccesary) for any programming, debugging, etc
* To generate SpecFlow BDD Living Docs (explained below), the `Specflow.Plus.LivingDoc.CLI` tool must be installed as per [the SpecFlow docs](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Installing-the-command-line-tool.html)
* Ability to run either *NIX shell script or Windows powershell to run `regen-living-docs.[sh|ps1]` script
* The ability to edit your local /etc/hosts file (or aprop version of this on Windows) to map the hostname `bus-homework.example` to `127.0.0.1`
  - This is done to simplify the CORS story and present an example of a gateway'd, containized solution that can transition to `k8s` hosting in the future
  - the webapp is designed to talk to a backend on the same hostname space
  - the `docker-compose.yml` is laid out in such a way that only the `gateway` component exposes public ports, and it will bind to port `80` on `localhost`; it is designed to route requests to `bus-homework.example`

## Sub-systems

* `api` - backend API, `rustlang`-based HTTP API; responds to requests from the `webapp` client
* `webapp` - a barebones React webapp, delivered at runtime by node.js; depends on `api`
* `gateway` - an nginx frontend, serves up access to `api` and `webapp` and depends on them
* `specs` - A `dotnet` sln root with a single project in the `src/BusHomework.Spec` directory
  - within the `Features` directory of this project are plain-language Gherkin-style `.feature` files covering tests that target `api` and `webapp` as black-box systems, tested in a full-integration context
  - running the `regen-living-docs.[sh|ps1]` script will build the project and run the BDD specs 
  - More about the SpecFlow.LivingDocs tool, etc can be learned at [the SpecFlow web site](https://specflow.org)
  - Web tests are ran via the `selenium-runner` component; straight-up API calls are done from .NET using HTTPClient
- `selenium-runner` a simple linux+chrome container that can act as a receiver of `WebDriver`-based selenium sessions, or as part of a larger selenium grid (if configured to do so)
  - For this repo, the container serves to support the `specs` component, which uses the Selenium.NET bindings to tests that run on `selenium-runner` and get the results back (and report them as part of the living docs)

## Missing functionality, nice to haves, etc
* The first thing I would add if this wasn't a time-constained POC would be authn/authz access control; `keycloak` is an obvious OSS solution supported by a major vendor; Okta, Auth0, etc would be SaaS vendor options, some reasons to avoid this:
  - One reason to skip is the homework constraint for no data-persistence component (which an authn/authz system implies)
  - All runtime components need to be updated to handle sessions, tokens, etc
  - It creates the need for an additional layer of test support infrastructure in the specs for token+session management; This is neccesary in any project that would be "production-ready", but it ommited for the sake of brevity
* TLS+SSL, encryption of internal traffic from the `gateway` component to it's internal peers; all stuff that would hopefully be dealt-with within a `k8s` install
* It would be nice to push the LivingDoc.html creation into a docker-based process that emits the file as a side-effect, but otherwise leaves in no side-effects (and makes the `dotnet`/SpecFlow dependencies go away)