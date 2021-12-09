<div style="margin-bottom: 1%; padding-bottom: 2%;">
	<img align="right" width="100px" src="https://dx29.ai/assets/img/logo-Dx29.png">
</div>

Dx29 API Gateway
==============================================================================================================================================

[![Build Status](https://f29.visualstudio.com/Dx29%20v2/_apis/build/status/DEV-MICROSERVICES/Dx29.APIGateway?branchName=develop)](https://f29.visualstudio.com/Dx29%20v2/_build/latest?definitionId=89&branchName=develop)

### **Overview**

Dx29 exposes an API for third parties to use the tool’s algorithms independently of the tool. That is, they will be able to run the calculations they need but not access the specific user and patient information stored in the databases or blobs.

It therefore offers the methods of the microservices that perform these functions, and simply acts as a link between an external application and the internal microservices located in the application cluster. 

It is used in the [Dx29 application](https://dx29.ai/) and therefore how to integrate it is described in the [Dx29 architecture guide](https://dx29-v2.readthedocs.io/en/latest/index.html).

It is programmed in C#, and the structure of the project is as follows:

>- src folder: This is made up of multimple folders which contains the source code of the project.
>>- Dx29.APIGateway. Where the controllers and methods exposed by this API are implemented. In addition, it will contain the necessary functionality to connect the API controllers and methods to the corresponding microservice.
>>- Dx29 and Dx29.Data. Used as libraries to add the common or more general functionalities used in Dx29 projects programmed in C#.
>- .gitignore file
>- README.md file
>- manifests folder: with the YAML configuration files for deploy in Azure Container Registry and Azure Kubernetes Service.
>- pipeline sample YAML file. For automatizate the tasks of build and deploy on Azure.

It is important to mention that for now this project includes the code of two projects that are no longer used (and therefore not necessary): Dx29.Bioenties and Dx29.TASearch. These functionalities have already been migrated to other projects in use.

<p>&nbsp;</p>

### **Getting Started**

####  1. Configuration: Pre-requisites

This project doesn’t need any secret value.
It needs to have the Docker images of the projects it exposes. 
>- [Dx29.Annotations](TODO_LINK)
>- [Dx29.BioNET](https://github.com/foundation29org/Dx29.BioNET)
>- [Dx29.BioEntity](https://github.com/foundation29org/Dx29.Bioentity)
>- [Dx29.TermSearch2](https://github.com/foundation29org/Dx29.TermSearch2)

<p>&nbsp;</p>

####  2. Download and installation

Download the repository code with `git clone` or use download button.

We use [Visual Studio 2019](https://docs.microsoft.com/en-GB/visualstudio/ide/quickstart-aspnet-core?view=vs-2022) for working with this project.

<p>&nbsp;</p>

####  3. Latest releases

The latest release of the project deployed in the [Dx29 application](https://dx29.ai/) is: v0.15.00.

<p>&nbsp;</p>

#### 4. API references
The methods that expose this API project can be consulted in this [link](http://dx29-api.northeurope.cloudapp.azure.com/index.html)

<p>&nbsp;</p>

### **Build and Test**

#### 1. Build

We could use Docker. 

Docker builds images automatically by reading the instructions from a Dockerfile – a text file that contains all commands, in order, needed to build a given image.

>- A Dockerfile adheres to a specific format and set of instructions.
>- A Docker image consists of read-only layers each of which represents a Dockerfile instruction. The layers are stacked and each one is a delta of the changes from the previous layer.

Consult the following links to work with Docker:

>- [Docker Documentation](https://docs.docker.com/reference/)
>- [Docker get-started guide](https://docs.docker.com/get-started/overview/)
>- [Docker Desktop](https://www.docker.com/products/docker-desktop)

The first step is to run docker image build. We pass in . as the only argument to specify that it should build using the current directory. This command looks for a Dockerfile in the current directory and attempts to build a docker image as described in the Dockerfile. 
```docker image build . ```

[Here](https://docs.docker.com/engine/reference/commandline/docker/) you can consult the Docker commands guide.

<p>&nbsp;</p>

#### 2. Deployment

To work locally, it is only necessary to install the project and build it using Visual Studio 2019. 

The deployment of this project in an environment is described in [Dx29 architecture guide](https://dx29-v2.readthedocs.io/en/latest/index.html), in the deployment section. In particular, it describes the steps to execute to work with this project as a microservice (Docker image) available in a kubernetes cluster:

1. Create an Azure container Registry (ACR). A container registry allows you to store and manage container images across all types of Azure deployments. You deploy Docker images from a registry. Firstly, we need access to a registry that is accessible to the Azure Kubernetes Service (AKS) cluster we are creating. For this purpose, we will create an Azure Container Registry (ACR), where we will push images for deployment.
2. Create an Azure Kubernetes cluster (AKS) and configure it for using the prevouos ACR
3. Import image into Azure Container Registry
4. Publish the application with the YAML files that defines the deployment and the service configurations. 

This project includes, in the Deployments folder, YAML examples to perform the deployment tasks as a microservice in an AKS. 

>>- **Interesting link**: [Deploy a Docker container app to Azure Kubernetes Service](https://docs.microsoft.com/en-GB/azure/devops/pipelines/apps/cd/deploy-aks?view=azure-devops&tabs=java)


<p>&nbsp;</p>

### **Contribute**

Please refer to each project's style and contribution guidelines for submitting patches and additions. The project uses [gitflow workflow](https://nvie.com/posts/a-successful-git-branching-model/). 
According to this it has implemented a branch-based system to work with three different environments. Thus, there are two permanent branches in the project:
>- The develop branch to work on the development environment.
>- The master branch to work on the test and production environments.

In general, we follow the "fork-and-pull" Git workflow.

>1. Fork the repo on GitHub
>2. Clone the project to your own machine
>3. Commit changes to your own branch
>4. Push your work back up to your fork
>5. Submit a Pull request so that we can review your changes

The project is licenced under the **(TODO: LICENCE & LINK & Brief explanation)**

<p>&nbsp;</p>
<p>&nbsp;</p>

<div style="border-top: 1px solid !important;
	padding-top: 1% !important;
    padding-right: 1% !important;
    padding-bottom: 0.1% !important;">
	<div align="right">
		<img width="150px" src="https://dx29.ai/assets/img/logo-foundation-twentynine-footer.png">
	</div>
	<div align="right" style="padding-top: 0.5% !important">
		<p align="right">	
			Copyright © 2020
			<a style="color:#009DA0" href="https://www.foundation29.org/" target="_blank"> Foundation29</a>
		</p>
	</div>
<div>
