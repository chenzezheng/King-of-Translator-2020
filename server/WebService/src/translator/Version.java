package translator;
import javax.jws.WebMethod;
import javax.jws.WebService;
import javax.xml.ws.Endpoint;

@WebService()
public class Version {
  @WebMethod
  public String getVersion(String os) {
    String result = "King of Translator v1.3 for " + os;
    System.out.println(result);
    return result;
  }
  public static void main(String[] argv) {
    Object implementor = new Version();
    String address = "http://localhost:9000/Version";
    Endpoint.publish(address, implementor);
  }
}
