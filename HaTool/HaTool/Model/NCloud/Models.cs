using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaTool.Model.NCloud
{

    public class changeLoadBalancedServerInstances
    {
        public changeLoadBalancedServerInstancesResponse changeLoadBalancedServerInstancesResponse { get; set; }
    }


    //public class KmsDecryptResponse
    //{
    //    public string code { get; set; }
    //    public string msg { get; set; }
    //    public DecryptedData data { get; set; }
    //}

    //public class DecryptedData
    //{
    //    public string plaintext { get; set; }
    //}

    //public class KmsEncryptResponse
    //{
    //    public string code { get; set; }
    //    public string msg { get; set; }
    //    public EncryptedData data { get; set; }
    //}

    //public class EncryptedData
    //{
    //    public string ciphertext { get; set; }
    //}



    public class changeLoadBalancedServerInstancesResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public int totalRows { get; set; }
        public List<loadBalancerInstance> loadBalancerInstanceList { get; set; }
    }

    public class deleteLoadBalancerInstances
    {
        public deleteLoadBalancerInstancesResponse deleteLoadBalancerInstancesResponse { get; set; }
    }

    public class deleteLoadBalancerInstancesResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public int totalRows { get; set; }
        public List<loadBalancerInstance> loadBalancerInstanceList { get; set; }
    }

    public class createLoadBalancerInstance
    {
        public createLoadBalancerInstanceResponse createLoadBalancerInstanceResponse { get; set; }
    }

    public class createLoadBalancerInstanceResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public int totalRows { get; set; }
        public List<loadBalancerInstance> loadBalancerInstanceList { get; set; }
    }


    public class getLoginKeyList
    {
        public getLoginKeyListResponse getLoginKeyListResponse { get; set; }
    }

    public class getLoginKeyListResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public int totalRows { get; set; }
        public List<loginKey> loginKeyList { get; set; }
    }

    public class getLoadBalancerInstanceList
    {
        public getLoadBalancerInstanceListResponse getLoadBalancerInstanceListResponse { get; set; }
    }

    public class getLoadBalancerInstanceListResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public int totalRows { get; set; }
        public List<loadBalancerInstance> loadBalancerInstanceList { get; set; }
    }

    public class loadBalancerInstance
    {
        public string loadBalancerInstanceNo { get; set; }
        public string virtualIp { get; set; }
        public string loadBalancerName { get; set; }
        public List<zone> zoneList { get; set; }
        public region region { get; set; }
        public codeCodeName loadBalancerAlgorithmType { get; set; }
        public string loadBalancerDescription { get; set; }
        public string createDate { get; set; }
        public string domainName { get; set; }
        public codeCodeName internetLineType { get; set; }
        public string loadBalancerInstanceStatusName { get; set; }
        public codeCodeName loadBalancerInstanceStatus { get; set; }
        public codeCodeName loadBalancerInstanceOperation { get; set; }
        public codeCodeName networkUsageType { get; set; }
        public bool isHttpKeepAlive { get; set; }
        public int connectionTimeout { get; set; }
        public string certificateName { get; set; }
        public List<loadBalancerRule> loadBalancerRuleList { get; set; }
    }

    public class loadBalancerRule
    {
        public codeCodeName protocolType { get; set; }
        public int loadBalancerPort { get; set; }
        public int serverPort { get; set; }
        public string l7HealthCheckPath { get; set; }
        public string certificateName { get; set; }
        public string proxyProtocolUseYn { get; set; }
    }

    public class getBlockStorageInstanceList
    {
        public getBlockStorageInstanceListResponse getBlockStorageInstanceListResponse { get; set; }
    }

    public class getBlockStorageInstanceListResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public int totalRows { get; set; }
        public List<blockStorageInstance> blockStorageInstanceList { get; set; }
    }

    public class blockStorageInstance
    {
        public string blockStorageInstanceNo { get; set; }
        public string serverInstanceNo { get; set; }
        public string serverName { get; set; }
        public codeCodeName blockStorageType { get; set; }
        public string blockStorageName { get; set; }
        public long blockStorageSize { get; set; }
        public string deviceName { get; set; }
        public string blockStorageProductCode { get; set; }
        public codeCodeName blockStorageInstanceStatus { get; set; }
        public codeCodeName blockStorageInstanceOperation { get; set; }
        public string blockStorageInstanceStatusName { get; set; }
        public string createDate { get; set; }
        public string blockStorageInstanceDescription { get; set; }
        public codeCodeName diskDetailType { get; set; }
        public int maxIopsThroughput { get; set; }
        public zone zone { get; set; }
    }

    public class loginKey
    {
        public string fingerprint { get; set; }
        public string keyName { get; set; }
        public string createDate { get; set; }
    }

    public class associatePublicIpWithServerInstance
    {
        public associatePublicIpWithServerInstanceResponse associatePublicIpWithServerInstanceResponse { get; set; }
    }

    public class associatePublicIpWithServerInstanceResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public int totalRows { get; set; }
        public List<publicIpInstance> publicIpInstanceList { get; set; }
    }


    public class disassociatePublicIpFromServerInstance
    {
        public disassociatePublicIpFromServerInstanceResponse disassociatePublicIpFromServerInstanceResponse { get;set;}
    }

    public class disassociatePublicIpFromServerInstanceResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public int totalRows { get; set; }
        public List<publicIpInstance> publicIpInstanceList { get; set; }
    }

    public class deletePublicIpInstances
    {
        public deletePublicIpInstancesResponse deletePublicIpInstancesResponse { get; set; }
    }

    public class deletePublicIpInstancesResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public int totalRows { get; set; }
        public List<publicIpInstance> publicIpInstanceList { get; set; }
    }


    public class startServerInstances
    {
        public startServerInstancesResponse startServerInstancesResponse { get; set; }
    }

    public class startServerInstancesResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public int totalRows { get; set; }
        public List<serverInstance> serverInstanceList { get; set; }
    }

    public class terminateServerInstances
    {
        public terminateServerInstancesResponse terminateServerInstancesResponse { get; set; }
    }

    public class terminateServerInstancesResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public int totalRows { get; set; }
        public List<serverInstance> serverInstanceList { get; set; }
    }

    public class createBlockStorageInstance
    {
        public createBlockStorageInstanceResponse createBlockStorageInstanceResponse { get; set; }
    }

    public class createBlockStorageInstanceResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public int totalRows { get; set; }
        public List<blockStorageInstance> blockStorageInstanceList { get; set; }

    }

    public class deleteBlockStorageInstances
    {
        public deleteBlockStorageInstancesResponse deleteBlockStorageInstancesResponse { get; set; }
    }

    public class deleteBlockStorageInstancesResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public int totalRows { get; set; }
        public List<blockStorageInstance> blockStorageInstanceList { get; set; }
    }


    public class createServerInstances
    {
        public createServerInstancesResponse createServerInstancesResponse { get; set; }
    }

    public class createServerInstancesResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public int totalRows { get; set; }
        public List<serverInstance> serverInstanceList { get; set; }
    }

    public class stopServerInstances
    {
        public stopServerInstancesResponse stopServerInstancesResponse { get; set; }
    }

    public class rebootServerInstances
    {
        public rebootServerInstancesResponse rebootServerInstancesResponse { get; set; }
    }

    public class stopServerInstancesResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public int totalRows { get; set; }
        public List<serverInstance> serverInstanceList { get; set; }
    }

    public class rebootServerInstancesResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public int totalRows { get; set; }
        public List<serverInstance> serverInstanceList { get; set; }
    }

    public class getPublicIpInstanceList
    {
        public getPublicIpInstanceListResponse getPublicIpInstanceListResponse { get; set; }
    }
    public class getPublicIpInstanceListResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public int totalRows { get; set; }
        public List<publicIpInstance> publicIpInstanceList { get; set; }
    }

    public class createPublicIpInstance
    {
        public createPublicIpInstanceResponse createPublicIpInstanceResponse { get; set; }
    }

    public class createPublicIpInstanceResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public int totalRows { get; set; }
        public List<publicIpInstance> publicIpInstanceList { get; set; }
    }

    public class publicIpInstance
    {
        public string publicIpInstanceNo { get; set; }
        public string publicIp { get; set; }
        public string publicIpDescription { get; set; }
        public codeCodeName internetLineType { get; set; }
        public string publicIpInstanceStatusName { get; set; }
        public codeCodeName publicIpInstanceStatus { get; set; }
        public codeCodeName publicIpInstanceOperation { get; set; }
        public codeCodeName publicIpKindType { get; set; }
        public serverInstance serverInstanceAssociatedWithPublicIp { get; set; }
        public zone zone { get; set; }
    }

    public class getServerInstanceList
    {
        public getServerInstanceListResponse getServerInstanceListResponse { get; set; }
    }

    public class getServerInstanceListResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public int totalRows { get; set; }
        public List<serverInstance> serverInstanceList { get; set; }
    }

    public class serverInstance
    {
        public string serverInstanceNo { get; set; }
        public string serverName { get; set; }
        public string publicIp { get; set; }
        public string privateIp { get; set; }
        public codeCodeName serverInstanceStatus { get; set; }
        public codeCodeName serverInstanceOperation { get; set; }
        public region region { get; set; }
        public zone zone { get; set; }
        public string serverImageProductCode { get; set; }
        public string serverProductCode { get; set; }
        public string feeSystemTypeCode { get; set; }
        public string loginKeyName { get; set; }
        public List<accessControlGroup> accessControlGroupList { get; set; }
        public codeCodeName serverInstanceType { get; set; }
        public codeCodeName internetLineType { get; set; }
        public codeCodeName platformType { get; set; }
        // and so many column...
        public override string ToString()
        {
            return serverName;
        }
    }

  

    public class getAccessControlGroupList
    {
        public getAccessControlGroupListResponse getAccessControlGroupListResponse { get; set; }
    }

    public class getAccessControlGroupListResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public int totalRows { get; set; }
        public List<accessControlGroup> accessControlGroupList { get; set; }
    }

    public class accessControlGroup
    {
        public string accessControlGroupConfigurationNo { get; set; }
        public string accessControlGroupName { get; set; }
        public string accessControlGroupDescription { get; set; }
        public bool isDefault { get; set; }
        public string createDate { get; set; }
        public override string ToString()
        {
            return accessControlGroupName;
        }
    }

    public class getZoneList
    {
        public getZoneListResponse getZoneListResponse { get; set; }
    }

    public class getZoneListResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public List<zone> zoneList { get; set; }
    }

    public class zone
    {
        public string zoneNo   { get; set; }
        public string zoneName { get; set; }
        public string zoneCode { get; set; }
        public string zoneDescription { get; set; }
        public string regionNo { get; set; }
        public override string ToString()
        {
            return zoneName;
        }
    }

    public class getServerProductList
    {
        public getServerProductListResponse getServerProductListResponse { get; set; }
    }

    public class getServerProductListResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public List<srvProduct> productList { get; set; }
    }

    public class srvProduct
    {
        public string productCode { get; set; }
        public string productName { get; set; }
        public codeCodeName productType { get; set; }
        public string productDescription { get; set; }
        public codeCodeName infraResourceType { get; set; }
        public int cpuCount { get; set; }
        public long memorySize { get; set; }
        public long baseBlockStorageSize { get; set; }
        public string osInformation { get; set; }
        public codeCodeName diskType { get; set; }
        public string dbKindCode { get; set; }
        public long addBlockStorageSize { get; set; }
        public override string ToString()
        {
            return productName;
        }
    }

    public class getServerImageProductList
    {
        public getServerImageProductListResponse getServerImageProductListResponse { get; set; }
    }

    public class getServerImageProductListResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public List<imgProduct> productList { get; set; }
    }

    public class imgProduct
    {
        public string productCode { get; set; }
        public string productName { get; set; }
        public codeCodeName productType { get; set; }
        public string productDescription { get; set; }
        public codeCodeName infraResourceType { get; set; }
        public int cpuCount { get; set; }
        public int memorySize { get; set; }
        public long baseBlockStorageSize { get; set; }
        public codeCodeName platformType { get; set; }
        public string osInformation { get; set; }
        public string dbKindCode { get; set; }
        public long addBlockStorageSize { get; set; }

        public override string ToString()
        {
            return productName;
        }
    }

    public class codeCodeName
    {
        public string code { get; set; }
        public string codeName { get; set; }
    }
    public class getRegionList
    {
        public getRegionListResponse getRegionListResponse { get; set; }
    }

    public class getRegionListResponse
    {
        public string requestId {get;set;}
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public string totalRows { get; set; }
        public List<region> regionList { get; set; }
    }

    public class region
    {
        public string regionNo { get; set; }
        public string regionCode { get; set; }
        public string regionName { get; set; }

        public override string ToString()
        {
            return regionName;
        }
    }

    public class createLoginKey
    {
        public createLoginKeyResponse createLoginKeyResponse { get; set; }
    }

    public class createLoginKeyResponse
    {
        public string requestId { get; set; }
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
        public string privateKey { get; set; }
    }


    public class hasError
    {
        public responseError responseError { get;set;}
    }

    public class authError
    {
        public error error { get; set; }
    }

    public class error
    {
        public string errorCode { get; set; }
        public string message { get; set; }
    }


    public class responseError
    {
        public string returnCode { get; set; }
        public string returnMessage { get; set; }
    }

}

