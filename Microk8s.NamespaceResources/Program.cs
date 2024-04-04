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

    namespaces.

    // export the deployment name
    return new Dictionary<string, object?>
    {
        //    ["name"] =  deployment.Metadata.Apply(m => m.Name)
    };
});
