#include "dateOfExpiry.h"
#include "personalData.h"

class card
{
protected:
	std::string cardNumber{};
	uint16_t* CVV{};
	uint16_t* balance{};
public:
	personalData* ownerData{};
	dateOfExpiry* dateExpiry{};

	card(personalData&, std::string&, uint16_t&, uint16_t&, dateOfExpiry&);

	void addBalance(uint16_t amount);

	std::string getCardNumber() const;
	uint16_t getCVV() const;
	uint16_t getBalance() const;

	//~card();
};