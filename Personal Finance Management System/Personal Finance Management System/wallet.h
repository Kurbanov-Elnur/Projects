#include "personalData.h"

class wallet
{
private:
	uint16_t* ID{};
	uint16_t* securityCode{};
public:
	personalData* ownerData{};
	uint16_t* balance{};
	std::string ownerEmail{};
	std::string ownerPhone{};
	std::string currency{};
	uint16_t* transactionLimit{};
	uint16_t* dailyLimit{};

	wallet(std::string&, std::string&, std::string&, uint16_t&, uint16_t&, uint16_t&, std::string&, std::string&, std::string&, uint16_t&
		, uint16_t&, uint16_t&, uint16_t&, uint16_t&);

	uint16_t getID() const;
	uint16_t getSecurityCode() const;

	~wallet();
};