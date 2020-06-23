using System;
using System.Linq;
using System.Text.RegularExpressions;

public class DomainNameValidator {
  public bool validate(String Domain) {  
    if (!isLengthValid(Domain)) {
      return false;
    }
    
    if (!isDotsCountValid(Domain)) {
      return false;
    }
    
    return areDomainPartsValid(Domain.Split('.'));
  }
  
  private bool isLengthValid(string Domain)
  {
    return Domain.Length <= 253;
  }
  
  private bool isDotsCountValid(string Domain) 
  {
      int dots = Domain.Count(c => c == '.');
      
      return dots >= 1 && dots <= 127;
  }
  
  private bool areDomainPartsValid(String[] parts)
  {        
    bool tldValid = true;  // TLD == last part
    
    foreach (var part in parts) {
        if (!isDomainPartValid(part)) {
            return false;
        }
        
        tldValid = isTldValid(part);
    }
    
    return tldValid;  
  }
  
  private bool isDomainPartValid(String part)
  {
      Regex levelRegexp = new Regex(@"^[a-zA-Z0-9]+([a-zA-Z0-9-]*[a-zA-Z0-9])?$");
      
      bool lengthValid = part.Length >= 1 && part.Length <= 63;
      bool formatValid = levelRegexp.IsMatch(part);
      
      return lengthValid && formatValid;
  }
  
  private bool isTldValid(String tld)
  {
      Regex allNum = new Regex(@"^[0-9]+$");
      
      return !allNum.IsMatch(tld);
  }
}
