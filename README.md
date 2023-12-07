![Selenium Docker](https://github.com/SeleniumHQ/docker-selenium/workflows/Build%20&%20test/badge.svg?branch=trunk)
![CI/CD](https://github.com/SeleniumHQ/docker-selenium/workflows/Deploys/badge.svg)
![Report](https://github.com/SeleniumHQ/docker-selenium/workflows/Lint%20and%20Test%20Helm%20Charts/badge.svg)

# Selenium C Sharp
The Repository is creating automation testing using [Selenium](https://www.selenium.dev/documentation/) from the scratch until implement the CI/CD.

##  Contents
* [Pre-Requisites](#pre-requisites)
* [How to Dockerize Selenium C#](#how-to-dockerize-selenium-c#)
* [How to Integrate Dockerize Selenium Grid C# with Jenkins](#how-to-integrate-dockerize-selenium-grid-c#-with-jenkins)

##  Pre-Requisites
1.  Have Installed [Visual Studio IDE 2022](https://visualstudio.microsoft.com/downloads/)
2.  Have Installed [NUNit Selenium C#](https://www.lambdatest.com/blog/nunit-testing-tutorial-for-selenium-csharp/) 4.0 or latest 
3.  Have Installed [Docker Desktop](https://www.docker.com/products/docker-desktop/) (Recommended to install the docker in the WSL Ubuntu due to the security)
4.  Have Installed [WSL Ubuntu](https://ubuntu.com/tutorials/install-ubuntu-on-wsl2-on-windows-11-with-gui-support)
5.  Have Downloaded [Jenkins](https://www.jenkins.io/download/) 

##  How to Dockerize Selenium C#

1.  Create **docker-compose.yml:**
``` bash
# To execute this docker-compose yml file use `docker-compose up`
# Add the `-d` flag at the end for detached execution
# To stop the execution, hit Ctrl+C, and then `docker-compose -f docker-compose down`

version: "3"
services:
  chrome:
    image: selenium/node-chrome:4.15.0-20231129
    shm_size: 2gb
    depends_on:
      - selenium-hub
    environment:
      - SE_EVENT_BUS_HOST=selenium-hub
      - SE_EVENT_BUS_PUBLISH_PORT=4442
      - SE_EVENT_BUS_SUBSCRIBE_PORT=4443
      - SE_NODE_MAX_SESSIONS=5
      - SE_NODE_STEREOTYPE={"browserName":"chrome","platformName":"Windows 11"}
  # edge:
  #   image: selenium/node-edge:4.15.0-20231129
  #   shm_size: 2gb
  #   depends_on:
  #     - selenium-hub
  #   environment:
  #     - SE_EVENT_BUS_HOST=selenium-hub
  #     - SE_EVENT_BUS_PUBLISH_PORT=4442
  #     - SE_EVENT_BUS_SUBSCRIBE_PORT=4443

  firefox:
    image: selenium/node-firefox:4.15.0-20231129
    shm_size: 2gb
    depends_on:
      - selenium-hub
    environment:
      - SE_EVENT_BUS_HOST=selenium-hub
      - SE_EVENT_BUS_PUBLISH_PORT=4442
      - SE_EVENT_BUS_SUBSCRIBE_PORT=4443
      - SE_NODE_MAX_SESSIONS=5
      - SE_NODE_STEREOTYPE={"browserName":"firefox","platformName":"Windows 11"}

  selenium-hub:
    image: selenium/hub:4.15.0-20231129
    container_name: selenium-hub
    ports:
      - "4442:4442"
      - "4443:4443"
      - "4444:4444"
```
2.  The Message on the following Screenshot should be appear:
![docker up](image.png)
3.  Visit selenium grid [Dashboard](http://localhost:4444/). The Nodes should be appear too.
4.  Run The Tests from Visual Studio 2022 

##  How to Integrate Dockerize Selenium Grid C# with Jenkins
1. Open `Command Prompt` and move the directory to jenkins insttallation((by default) `C:\Program Files\Jenkins` ). Then execute this command:
```bash
java -jar Jenkins.war
```
2. The Following Message be appear:
```bash
hudson.lifecycle.Lifecycle#onReady: Jenkins is fully up and running
```
3. Visit Jenkins [Dashboard](http://localhost:8080) by default.
4. Set up your new Project and integrate with your local project by following this [Tutorial](https://www.youtube.com/watch?v=LLydAZ01eEg)
5. Since the Docker is still in Local, please run docker on your local before run  the build on jenkins.

##  How to Integrate GitHub Webhook with Jenkins
On this Section we are trying to trigger to run build when the changes has been pushed to the specific mentioned branch on `Github`.
1. Set up your Github Webhook integration by following this [Tutorial](https://www.youtube.com/watch?v=bE_vbKI3FrU)
2. Create and push the changes to the `Github` and the Build should be triggered.
