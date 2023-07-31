#include "wallet.h"
#include <regex>

namespace functions
{
	void myCheck(std::string& str, std::regex regexCheck);
	wallet addWallet();
	personalData addPersonalData();
	dateOfExpiry addDateOfExpiry();
};