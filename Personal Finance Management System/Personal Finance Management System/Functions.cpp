#pragma warning(disable : 4996)
#include "Functions.h"
#include <ctime>

void functions::myCheck(std::string& str, std::regex regexCheck)
{
	while (true)
	{
		if (!std::regex_match(str, regexCheck)) {
			std::cout << "Invalid input!" << std::endl;
			std::cout << "Please re-enter: " << std::endl; std::cin >> str;
			continue;
		}
		break;
	}
}

wallet functions::addWallet()
{
	std::string ownerEmail{}, ownerPhone{}, currency{};

	std::string checkData[3];

	personalData person = functions::addPersonalData();

	std::cout << "Enter owner email: " << std::endl; std::cin >> ownerEmail;
	functions::myCheck(ownerEmail, std::regex(R"(([a-zA-Z0-9](.|_)?)+([a-zA-Z0-9])+@([a-zA-Z0-9])+((.)[a-zA-Z]{2,})+)"));

	std::cout << "Enter owner phone: " << std::endl; std::cin >> ownerPhone;
	functions::myCheck(ownerPhone, std::regex("^[+]994[0-9]{9}$"));

	std::cout << "Enter wallet currency: " << std::endl; std::cin >> currency;
	functions::myCheck(currency, std::regex("[A-Z]{2,20}"));

	std::cout << "Enter wallet id: " << std::endl; std::cin >> checkData[0];
	functions::myCheck(checkData[0], std::regex("[0-9]{8}"));
	uint16_t ID = std::stoi(checkData[0]);

	std::cout << "Enter security code: "; std::cin >> checkData[1];
	functions::myCheck(checkData[1], std::regex("[0-9]{4}"));
	uint16_t securityCode = std::stoi(checkData[1]);

	std::cout << "Enter balance: " << std::endl; std::cin >> checkData[2];
	functions::myCheck(checkData[2], std::regex("[0-9]{1,4}[.]?[0-9]{1,2}"));
	float balance = std::stoi(checkData[2]);

	wallet w(person, ownerEmail, ownerPhone, currency, ID, securityCode, balance);

	return w;
}

personalData functions::addPersonalData()
{
	std::string name{}, surname{}, patronomic{}, checkDayOfBirth{}, checkMonthOfBirth{}, checkYearOfBirth{};
	uint16_t dayOfBirth{}, monthOfBirth{}, yearOfBirth{};

	std::cout << "Enter the owner's name: " << std::endl; std::cin >> name;
	functions::myCheck(name, std::regex("[A-Z]{2,20}"));

	std::cout << "Enter owner surname: " << std::endl; std::cin >> surname;
	functions::myCheck(surname, std::regex("[A-Z]{2,20}"));

	std::cout << "Enter the patronymic of the owner: " << std::endl; std::cin >> patronomic;
	functions::myCheck(patronomic, std::regex("[A-Z]{2,20}"));

	while(dayOfBirth == 0 || dayOfBirth > 31)
	{
		std::cout << "Enter the owner's birthday: " << std::endl; std::cin >> checkDayOfBirth;
		functions::myCheck(checkDayOfBirth, std::regex("[0-9]{1,2}"));
		dayOfBirth = std::stoi(checkDayOfBirth);
	}

	while (monthOfBirth == 0 || monthOfBirth > 12)
	{
		std::cout << "Enter the owner's month of birth: " << std::endl; std::cin >> checkMonthOfBirth;
		functions::myCheck(checkMonthOfBirth, std::regex("[0-9]{1,2}"));
		monthOfBirth = std::stoi(checkMonthOfBirth);
	}

	std::time_t now = std::time(nullptr);
	std::tm* timeinfo = std::localtime(&now);
	int currentYear = timeinfo->tm_year + 1900;

	while (yearOfBirth == 0 || currentYear - yearOfBirth < 18)
	{
		std::cout << "Enter the owner's year of birth: " << std::endl; std::cin >> checkYearOfBirth;
		functions::myCheck(checkYearOfBirth, std::regex("[0-9]{4}"));
		yearOfBirth = std::stoi(checkYearOfBirth);
	}
	
	personalData p(name, surname, patronomic, dayOfBirth, monthOfBirth, yearOfBirth);

	return p;
}

dateOfExpiry functions::addDateOfExpiry()
{
	uint16_t monthOfExpiry{}, yearOfExpiry{};
	std::string checkMonthOfExpiry{}, checkYearOfExpiry{};

	std::time_t now = std::time(nullptr);
	std::tm* timeinfo = std::localtime(&now);
	int currentYear = timeinfo->tm_year + 1900;

	while (monthOfExpiry == 0 || monthOfExpiry > 12)
	{
		std::cout << "Enter month of expiry: " << std::endl; std::cin >> checkMonthOfExpiry;
		functions::myCheck(checkMonthOfExpiry, std::regex("[0-9]{1,2}"));
		monthOfExpiry = std::stoi(checkMonthOfExpiry);
	}

	while (yearOfExpiry == 0 || currentYear - yearOfExpiry > 10 )
	{
		std::cout << "Enter year of expiry: " << std::endl; std::cin >> checkYearOfExpiry;
		functions::myCheck(checkYearOfExpiry, std::regex("[0-9]{4}"));
		yearOfExpiry = std::stoi(checkYearOfExpiry);
	}

	dateOfExpiry d(monthOfExpiry, yearOfExpiry);

	return d;
}