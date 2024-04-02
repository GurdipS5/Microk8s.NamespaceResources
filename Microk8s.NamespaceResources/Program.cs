using Pulumi;
using Pulumi.Kubernetes.Types.Inputs.Core.V1;
using Pulumi.Kubernetes.Types.Inputs.Apps.V1;
using Pulumi.Kubernetes.Types.Inputs.Meta.V1;
using System.Collections.Generic;

return await Deployment.RunAsync(() =>
{
    var appLabels = new InputMap<string>
    {
        { "app", "nginx" }
    };

    var namespaces = new Pulumi.Kubernetes.Core.V1.NamespaceList("");



    var deployment = new Pulumi.Kubernetes.Core.V1.NamespaceList("nginx", new DeploymentArgs
    {


        Spec = new DeploymentSpecArgs
        {
            Selector = new LabelSelectorArgs
            {
                MatchLabels = appLabels
            },
            Replicas = 1,
            Template = new PodTemplateSpecArgs
            {
                Metadata = new ObjectMetaArgs
                {
                    Labels = appLabels
                },
                Spec = new PodSpecArgs
                {
                    Containers =
                    {
                        new ContainerArgs
                        {
                            Name = "nginx",
                            Image = "nginx",
                            Ports =
                            {
                                new ContainerPortArgs
                                {
                                    ContainerPortValue = 80
                                }
                            }
                        }
                    }
                }
            }
        }
    });

    // export the deployment name
    return new Dictionary<string, object?>
    {
        ["name"] =  deployment.Metadata.Apply(m => m.Name)
    };
});
